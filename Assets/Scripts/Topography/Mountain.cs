using UnityEngine;

public class Mountain : Topography
{
    public int peakHeight;
    public bool isClimbable;
    public bool blockPath;

    void Start()
    {
        objectName = "Mountain";
        size = 4; // can be different
        affectsMovement = true;
        peakHeight = 10; // can be different
        isClimbable = false;
        blockPath = true;
    }
}
