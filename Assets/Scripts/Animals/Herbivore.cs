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
                    Debug.Log(animalName + " consumed " + plantScript.plantName);
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
            if (currentHunger < 20)
            {
                currentHunger--;
                MoveToClosestConsumablePlant();
            }
            else if (currentHunger > 0)
            {
                currentHunger--;
            }
        }
    }
}