using UnityEngine;

public class Locust : Plant
{
    protected override void InitializePlant()
    {
        plantName = "Locust";
        waterRequirement = false;
        isReadyToConsume = true;
        regrowthInterval = 11f;
        timer = 0f;
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not initialized in InitializePlant on " + gameObject.name);

        }
    }

    // Check if the plant is ready to regrow
    protected override void CheckGrowth()
    {
        timer += Time.deltaTime;
        if (isReadyToConsume == false && timer >= regrowthInterval)
        {
            timer = 0f;
            isReadyToConsume = true;
            ChangeSprite(defaultSprite, new Vector2(0.15f, 0.15f));
        }
    }
}