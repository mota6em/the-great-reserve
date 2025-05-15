using UnityEngine;

public class GameEndScript : MonoBehaviour
{
    public ShopScript shopScript;

    private int requiredMoney;
    private int requiredAnimals;

    void Start()
    {
        int difficulty = PlayerPrefs.GetInt("SelectedDifficulty", 0);

        switch (difficulty)
        {
            case 0: // Easy
                requiredMoney = 10000;
                requiredAnimals = 10;
                break;
            case 1: // Medium
                requiredMoney = 20000;
                requiredAnimals = 20;
                break;
            case 2: // Hard
                requiredMoney = 30000;
                requiredAnimals = 30;
                break;
            default:
                requiredMoney = 10000;
                requiredAnimals = 10;
                break;
        }

        if (shopScript == null)
        {
            shopScript = Object.FindFirstObjectByType<ShopScript>();
        }
    }

    void Update()
    {
        int currentMoney = shopScript != null ? shopScript.GetMoney() : 0;
        int animalCount = GameObject.FindGameObjectsWithTag("Herbivore").Length +
                          GameObject.FindGameObjectsWithTag("Carnivore").Length;

        if (currentMoney >= requiredMoney && animalCount >= requiredAnimals)
        {
            Debug.Log("You win! Game over.");
        }
    }
}