using UnityEngine;

public class River : Topography
{
    void Start()
    {
        objectName = "River";
        size = 2; //can be different
        affectsMovement = true;
    }

    public void AffectCrossing()
    {
        // the logic here
    }
}
