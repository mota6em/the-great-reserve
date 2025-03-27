using UnityEngine;

public class Hill : Topography
{
    public int height;

    void Start()
    {
        objectName = "Hill";
        size = 2; // can be different
        affectsMovement = true;
        height = 3; // can be different
    }

    public void AffectVisibility()
    {
        // the logic here
    }
}
