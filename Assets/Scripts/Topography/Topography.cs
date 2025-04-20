using UnityEngine;

public abstract class Topography : MonoBehaviour
{
    [SerializeField] protected string topographyName;
    [SerializeField] protected int size;
    [SerializeField] protected bool affectsMovement;
     
    public virtual void Initialize(string name, int size, bool affectsMovement)
    {
        this.topographyName = name;
        this.size = size;
        this.affectsMovement = affectsMovement;
    }
}
