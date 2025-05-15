using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public abstract class Animal : Characters
{
    public void DeleteAnimal()
    {
        Destroy(gameObject);
    }

    public string GetAnimalName() { return characterName; }

    private void OnMouseDown()
    {
        if (uiManager != null)
        {
            uiManager.OpenAnimalDataPanel();
            uiManager.SelectAnimal(this);
        }
    }

}