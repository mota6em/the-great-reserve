using UnityEngine;

public class GameEndScript : MonoBehaviour
{
    public ShopScript shopScript;
    public GameObject gameEndPanel;
    private int requiredMoney;
    private int requiredAnimals;
    private TimeManager timeManager;

    void Start()
    {
        int difficulty = PlayerPrefs.GetInt("SelectedDifficulty", 0);

        switch (difficulty)
        {
            case 0: // Easy
                requiredMoney = 7000;
                requiredAnimals = 3;
                break;
            case 1: // Medium
                requiredMoney = 9000;
                requiredAnimals = 10;
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

        timeManager = Object.FindFirstObjectByType<TimeManager>();
    }

    void Update()
    {
        int currentMoney = shopScript != null ? shopScript.GetMoney() : 0;
        int animalCount = GameObject.FindGameObjectsWithTag("Herbivore").Length +
                          GameObject.FindGameObjectsWithTag("Carnivore").Length;

        //Debug.Log("Current Money: " + currentMoney + " Animal count: " + animalCount);
        if (currentMoney >= requiredMoney && animalCount >= requiredAnimals)
        {
            Debug.Log("You win! Game over.");

            if (gameEndPanel != null)
                gameEndPanel.SetActive(true);

            if (timeManager != null)
                timeManager.PauseGame();

            enabled = false;
        }
    }
}