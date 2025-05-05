using UnityEngine;

public abstract class Tourist : Human
{
    public bool isAssignedToJeep = false;
    void start()
    {
        name = "Tourist";
        energy = 100;
    }
}
