using UnityEngine;

public class Lake : Topography
{
    void Start()
    {
        objectName = "Lake";
        size = 3; // can be different
        affectsMovement = true;
    }
}

