using UnityEngine;

public class Lake : Topography
{
    void Start()
    {
        topographyName = "Lake";
        size = 2;  
        affectsMovement = true;
    }

    public override void affectCrossing(){}
}
