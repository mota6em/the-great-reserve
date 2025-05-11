using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public abstract class People : Characters
{
    public string GetPersonName() { return characterName; }

    // checks if the Target is in range
    public bool TargetInRange(GameObject target, float range)
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance < range)
        {
            return true;
        }
        return false;
    }


}