using TMPro;
using UnityEngine;

public class Camel : Herbivore
{
    protected override void InitializeAnimal()
    {
        animalName = "Camel";
        age = 5;
        visionRange = 10;
        maxThirst = 150;
        currentThirst = 150;
        maxHealth = 100;
        currentHealth = 100;
        maxHunger = 100;
        currentHunger = 25;

        moveSpeed = 2.0f;
        noiseOffsetX = Random.Range(0f, 100f);
        noiseOffsetY = Random.Range(0f, 100f);
        standTimer = 0f;
        standDuration = 5f;
        timeBetweenStopsTimer = 0f;
        timeBetweenStops = 7f;

       // MoveToClosestConsumablePlant();
       // Debug.Log(FindClosestConsumablePlant());
    }
}