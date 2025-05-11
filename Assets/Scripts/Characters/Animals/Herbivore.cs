using TMPro;
using UnityEngine;

public abstract class Herbivore : Animal
{
    //Finds the closest consumable plant in the scene and returns its position
    protected Vector2 FindClosestConsumablePlant()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        GameObject closestPlant = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject plant in plants)
        {
            Plant plantScript = plant.GetComponent<Plant>();
            if (plantScript != null && plantScript.isReadyToConsume)
            {
                float distance = Vector2.Distance(transform.position, plant.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlant = plant;
                }
            }
        }

        if (closestPlant != null)
        {
            return closestPlant.transform.position;
        }
        else
        {
            return Vector2.zero;
        }
    }

    // Moves the animal to the closest consumable plant
    protected void MoveToClosestConsumablePlant()
    {
        Vector2 plantPosition = FindClosestConsumablePlant();
        if (plantPosition != Vector2.zero)
        {
            MoveToLocation(plantPosition);
        }
    }

    // This method is called when the animal reaches its target (the plant)
    protected override void OnTargetReached()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        foreach (GameObject plant in plants)
        {
            if (Vector2.Distance(transform.position, plant.transform.position) < 0.1f)
            {
                Plant plantScript = plant.GetComponent<Plant>();
                if (plantScript != null && plantScript.isReadyToConsume)
                {
                    plantScript.Consume();
                    currentHunger = maxHunger;
                    PausePerlinNoiseMovement(standDuration);
                    Debug.Log(characterName + " consumed " + plantScript.plantName);
                    break;
                }
            }
        }
    }

    protected override void HandleHunger()
    {
        hungerTimer += Time.deltaTime;
        if (hungerTimer >= hungerInterval)
        {
            hungerTimer = 0f;

            // Éhség csökkentése minden esetben
            if (currentHunger > 0)
            {
                currentHunger--;
            }

            // Ha az éhség 0, csökkentsük az életerõt
            if (currentHunger == 0)
            {
                if (currentHealth > 0)
                {
                    currentHealth--;
                    Debug.Log($"{characterName} is starving! Health decreased to {currentHealth}.");
                }
                else
                {
                    DeleteAnimal();
                    Debug.Log($"{characterName} died from starvation!");
                }
            }
            // Ha az éhség nagyobb, mint a küszöbérték, növeljük az életerõt
            else if (currentHunger > hungerThreshold)
            {
                if (currentHealth < maxHealth)
                {
                    currentHealth++;
                    Debug.Log($"{characterName} is well-fed! Health increased to {currentHealth}.");
                }
            }
            // Ha az éhség kisebb vagy egyenlõ a küszöbértékkel, keressünk növényevõt
            if (currentHunger <= hungerThreshold)
            {
                MoveToClosestConsumablePlant();
            }
        }
    }
}