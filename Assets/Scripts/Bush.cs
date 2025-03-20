using UnityEngine;

public class Bush : Plant
{
    protected override void InitializePlant()
    {
        plantName = "Bush";
        waterRequirement = false;
        isReadyToConsume = false;
        regrowthInterval = 10f;
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

    //changes render 
    private void changeRender()
    {

    }
}
