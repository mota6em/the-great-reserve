using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text herbivoreCountText;
    public Text carnivoreCountText;
    public Text plantCountText;

    public Text animalNameText;
    public Text animalAgeText;
    public Text animalVisionRangeText;
    public Text animalThirstText;
    public Text animalHealthText;
    public Text animalHungerText;
    public Text animalMovementSpeedText;
    private Animal selectedAnimal;

    public GameObject animalDataPanel;
    public GameObject gameSpeedPanel;

    // Update is called once per frame
    void Update()
    {
        UpdateCounts();
        if (selectedAnimal != null)
        {
            UpdateAnimalInfo(selectedAnimal);
        }
    }

    private void UpdateCounts()
    {
        int herbivoreCount = GameObject.FindGameObjectsWithTag("Herbivore").Length;
        int carnivoreCount = GameObject.FindGameObjectsWithTag("Carnivore").Length;
        int plantCount = GameObject.FindGameObjectsWithTag("Plant").Length;

        herbivoreCountText.text = "Herbivores: " + herbivoreCount;
        carnivoreCountText.text = "Carnivores: " + carnivoreCount;
        plantCountText.text = "Plants: " + plantCount;
    }

    public void SelectAnimal(Animal animal)
    {
        selectedAnimal = animal;
    }

    private void UpdateAnimalInfo(Animal animal)
    {
        animalNameText.text = animal.GetAnimalName();
        animalHealthText.text = "Health: " + animal.GetCurrentHealth() + "/" + animal.GetMaxHealth();
        animalHungerText.text = "Hunger: " + animal.GetCurrentHunger() + "/" + animal.GetMaxHunger();
        animalThirstText.text = "Thirst: " + animal.GetCurrentThirst() + "/" + animal.GetMaxThirst();
        animalVisionRangeText.text = "Vision Range: " + animal.GetVisionRange();
        animalMovementSpeedText.text = "Movement Speed: " + animal.GetMoveSpeed();
        animalAgeText.text = "Age: " + animal.GetAge(); 
    }

    public void OpenAnimalDataPanel()
    {
        animalDataPanel.SetActive(true);
    }

    public void CloseAnimalDataPanel()
    {
        animalDataPanel.SetActive(false);
    }

    public void ToggleGameSpeedPanel()
    {
        gameSpeedPanel.SetActive(!gameSpeedPanel.activeSelf);
    }
    public void SellAnimal()
    {
        if (selectedAnimal != null)
        {
            selectedAnimal.DeleteAnimal();
            selectedAnimal = null;
            CloseAnimalDataPanel();
        }
    }
    public Animal GetSelectedAnimal()
    {
        return selectedAnimal;
    }
}
