using TMPro;
using UnityEngine;

public class Lion : Carnivore
{
    protected override void InitializeAnimal()
    {
        animalName = "Lion";
        age = 5;
        visionRange = 10;
        maxThirst = 150;
        currentThirst = 150;
        maxHealth = 100;
        currentHealth = 100;
        maxHunger = 100;
        currentHunger = 100;

        moveSpeed = 2.0f;
        noiseOffsetX = Random.Range(0f, 100f);
        noiseOffsetY = Random.Range(0f, 100f);
        standTimer = 0f;
        standDuration = 5f;
        timeBetweenStopsTimer = 0f;
        timeBetweenStops = 7f;

    }
}