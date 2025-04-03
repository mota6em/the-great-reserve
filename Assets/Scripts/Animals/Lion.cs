using TMPro;
using UnityEngine;

public class Lion : Carnivore
{
    protected override void InitializeAnimal()
    {
        moveSpeed = 2.0f;
        noiseOffsetX = Random.Range(0f, 100f);
        noiseOffsetY = Random.Range(0f, 100f);
        standTimer = 0f;
        standDuration = 5f;
        timeBetweenStopsTimer = 0f;
        timeBetweenStops = 7f;
    }
}