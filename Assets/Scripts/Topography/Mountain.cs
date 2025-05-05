using UnityEngine;

public class Mountain : Topography
{
    public int peakHeight = 5;
    public bool isClimbable = false;
    public bool blockPath = true;

    void Start()
    {
        topographyName = "Mountain";
        size = 4;
        affectsMovement = true;
    }
}
