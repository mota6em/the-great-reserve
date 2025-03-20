using UnityEngine;

public class Grass : Plant
{
    protected override void InitializePlant()
    {
        plantName = "Grass";
        waterRequirement = false;
        isReadyToConsume = false;
        regrowthInterval = 5f;
        timer = 0f;
    }

    //Check if the plant is ready to regrow
    protected override void CheckGrowth()
    {
        timer += Time.deltaTime;
        if (isReadyToConsume == false && timer >= regrowthInterval)
        {
            timer = 0f;
            isReadyToConsume = true;
            changeRender();
        }
    }

    //changes 
    private void changeRender()
    {

    }
}
