using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject plantsPanel;
    public GameObject animalsPanel;
    public GameObject jeepsPanel;
    public GameObject terrarianPanel;
    public GameObject money;
    private int currentMoney = 0;
    private float timer = 0f;
    private float interval = 1f;
    private bool isPlacingPlant = false;
    private GameObject plantPrefab;
    private int plantCost;

    private Dictionary<string, int> plantPrices = new Dictionary<string, int>
    {
        { "Bush", 100 },
        { "Grass", 50 },
    };

    private Dictionary<string, Button> plantButtons = new Dictionary<string, Button>();

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            AddMoney(1);
            timer = 0;
        }

        if (isPlacingPlant && Input.GetMouseButtonDown(0))
        {
            PlacePlant();
        }
    }

    void Start()
    {
        AddMoney(500);
        InitializePlantButtons();
        UpdateButtonLabels();
    }

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }

    public void ShowPanel(string panelName)
    {
        plantsPanel.SetActive(panelName == "plants");
        animalsPanel.SetActive(panelName == "animals");
        jeepsPanel.SetActive(panelName == "jeeps");
        terrarianPanel.SetActive(panelName == "terrarian");
    }

    public void AddMoney(int value)
    {
        currentMoney += value;
        money.GetComponent<Text>().text = "Money: " + currentMoney.ToString();
    }

    public void SetPlantPrefab(GameObject plant)
    {
        plantPrefab = plant;
    }

    public void SelectPlantToPlace(string plantName)
    {
        if (plantPrices.TryGetValue(plantName, out int cost))
        {
            if (currentMoney >= cost)
            {
                plantCost = cost;
                isPlacingPlant = true;
            }
            else
            {
                Debug.Log("Not enough money to buy this plant.");
            }
        }
        else
        {
            Debug.Log("Plant not found.");
        }
    }

    private void PlacePlant()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null)
        {
            Instantiate(plantPrefab, hit.point, Quaternion.identity);
            currentMoney -= plantCost;
            money.GetComponent<Text>().text = "Money: " + currentMoney.ToString();
            isPlacingPlant = false;
        }
        else
        {
            Debug.Log("No valid position to place plant.");
        }
    }

    private void InitializePlantButtons()
    {
        foreach (Transform child in plantsPanel.transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                string plantName = button.name;
                if (plantPrices.ContainsKey(plantName))
                {
                    plantButtons[plantName] = button;
                }
                else
                {
                    Debug.LogWarning("Plant name not found in plantPrices: " + plantName);
                }
            }
            else
            {
                Debug.LogWarning("Button component not found on: " + child.name);
            }
        }
    }

    private void UpdateButtonLabels()
    {
        foreach (var plant in plantPrices)
        {
            if (plantButtons.TryGetValue(plant.Key, out Button button))
            {
                button.GetComponentInChildren<Text>().text = plant.Key + " (" + plant.Value + ")";
            }
        }
    }
}