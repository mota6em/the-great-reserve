using TMPro;
using UnityEngine;

public class Camel : Herbivore
{
    protected override void InitializeAnimal()
    {
        animalName = "Camel";
        age = 5;
        visionRange = 10;
        thirst = 100;
        maxHealth = 100;
        currentHealth = 100;
        hunger = 100;

        moveSpeed = 2.0f;
        noiseOffsetX = Random.Range(0f, 100f);
        noiseOffsetY = Random.Range(0f, 100f);
        standTimer = 0f;
        standDuration = 5f;
        timeBetweenStopsTimer = 0f;
        timeBetweenStops = 7f;
    }
}