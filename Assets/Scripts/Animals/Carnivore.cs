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

                Debug.Log($"{animalName} has eaten {herbivoreAnimal.GetAnimalName()} and refilled its hunger.");
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
            if (currentHunger < 20)
            {
                currentHunger--;
                MoveToClosestHerbivoreAnimal();
            }
            else if (currentHunger > 0)
            {
                currentHunger--;
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