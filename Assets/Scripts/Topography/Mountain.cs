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

    public override void affectCrossing()
    {
        if (blockPath)
        {
            Debug.Log("Mountain blocks the path.");
        }
        else if (!isClimbable)
        {
            Debug.Log("Mountain is too steep to climb.");
        }
        else
        {
            Debug.Log("Climbing the mountain...");
        }
    }
}
