using TMPro;
using UnityEngine;

public abstract class Carnivore : Animal
{
    private GameObject targetHerbivore = null;

    private new void Update()
    {
        base.Update();

        if (targetHerbivore != null)
        {
            MoveToLocation(targetHerbivore.transform.position);

            if (Vector2.Distance(transform.position, targetHerbivore.transform.position) < 0.1f)
            {
                OnTargetReached();
                targetHerbivore = null;
            }
        }
    }
    protected override void OnTargetReached()
    {
        if (targetHerbivore != null)
        {
            Animal herbivoreAnimal = targetHerbivore.GetComponent<Animal>();
            if (herbivoreAnimal != null)
            {
                herbivoreAnimal.DeleteAnimal();

                currentHunger = maxHunger;
                PausePerlinNoiseMovement(standDuration);
                Debug.Log($"{characterName} has eaten {herbivoreAnimal.GetAnimalName()} and refilled its hunger.");
            }
            targetHerbivore = null;
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
                MoveToClosestHerbivoreAnimal();
            }
        }
    }

    protected void MoveToClosestHerbivoreAnimal()
    {
        GameObject[] herbivores = GameObject.FindGameObjectsWithTag("Herbivore");

        if (herbivores.Length == 0)
        {
            Debug.LogWarning("No herbivores found in the scene.");
            targetHerbivore = null;
            return;
        }

        GameObject closestHerbivore = null;
        float closestDistance = float.MaxValue;

        foreach (var herbivore in herbivores)
        {
            float distance = Vector2.Distance(transform.position, herbivore.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHerbivore = herbivore;
            }
        }

        if (closestHerbivore != null)
        {
            targetHerbivore = closestHerbivore;
        }
    }
}