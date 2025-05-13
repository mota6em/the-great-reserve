using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public abstract class People : Characters
{
    public string GetPersonName() { return characterName; }

    // checks if the Target is in range
    protected bool TargetInRange(GameObject target, float range)
    {
        return Vector3.Distance(transform.position, target.transform.position) <= range;
    }

}