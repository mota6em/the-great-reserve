using UnityEngine;

public class Bush : Plant
{
    protected override void InitializePlant()
    {
        plantName = "Bush";
        waterRequirement = false;
        isReadyToConsume = true;
        //don't change this variable to a number below 10
        regrowthInterval = 11f;
        timer = 0f;
        if (spriteRenderer != null)
        {
            Consume();
        }
        else
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
            ChangeSprite(defaultSprite, new Vector2(0.4f, 0.4f));
        }
    }

    //this is called when something consumes the plant, or you place the plant. It sets the isReadyToConsume to false and changes skin
    public void Consume()
    {
        if (isReadyToConsume)
        {
            isReadyToConsume = false;
            ChangeSprite(growingSprite, new Vector2(0.2f, 0.2f));
        }
    }
}