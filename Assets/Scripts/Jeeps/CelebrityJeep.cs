using UnityEngine;

public class CelebrityJeep : Jeep
{
    private bool isWaitingForTourists = false;
    private bool hasTourists = false;

    override
    public void TryGetTourists()
    {
        if (hasTourists) return;

        TouristManager tm = FindObjectOfType<TouristManager>();
        if (tm != null)
        {
            int touristCount = tm.AssignTourists(1);

            if (touristCount > 0)
            {
                isWaitingForTourists = false;
                hasTourists = true;
            }
            else
            {
                isWaitingForTourists = true;
                hasTourists = false;
                Debug.Log($"{name} is waiting for a celebrity tourist...");
            }
        }
    }

    override
    public void wait()
    {
        isWaitingForTourists = true;
    }

    override
    public void move()
    {
        // Intentionally empty — movement handled by CelebrityJeepFollower
    }
}
