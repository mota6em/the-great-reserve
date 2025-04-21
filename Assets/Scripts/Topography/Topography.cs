using UnityEngine;

public abstract class Topography : MonoBehaviour
{
    protected string topographyName;
    protected int size;
    public bool affectsMovement;

    public abstract void affectCrossing();
}
