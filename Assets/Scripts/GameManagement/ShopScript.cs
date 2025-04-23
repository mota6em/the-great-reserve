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
    private bool isPlacingItem = false;
    private GameObject itemPrefab;
    private int itemCost;

    private Dictionary<string, int> itemPrices = new Dictionary<string, int>
    {
        //animals
        { "Leopard", 150 },
        { "Lion", 200 },
        { "Elephant", 300 },
        { "Giraffe", 250 },
        { "Zebra", 200 },

        { "Camel", 100 },
        //plants
        { "Bush", 100 },
        { "Grass", 50 },

        //jeeps
        { "Jeep2", 50 },
        { "Jeep4", 150 },

        //terrarian
        { "River", 200 },
        { "Mountain", 200 },
    };

    private Dictionary<string, Button> itemButtons = new Dictionary<string, Button>();

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            AddMoney(1);
            timer = 0;
        }

        if (isPlacingItem && Input.GetMouseButtonDown(0))
        {
            PlaceItem();
        }
    }

    void Start()
    {
        AddMoney(5000);
        InitializeItemButtons();
        UpdateButtonLabels();
    }

    //Toggles/hides the shop panel
    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }

    //Switches between category panels
    public void ShowPanel(string panelName)
    {
        plantsPanel.SetActive(panelName == "plants");
        animalsPanel.SetActive(panelName == "animals");
        jeepsPanel.SetActive(panelName == "jeeps");
        terrarianPanel.SetActive(panelName == "terrarian");
    }

    //Adds "value" amount of money to the players balance
    public void AddMoney(int value)
    {
        currentMoney += value;
        money.GetComponent<Text>().text = "Money: " + currentMoney.ToString();
    }

    //When called, it sets the current itemPrefab to the item in the argument
    public void SetItemPrefab(GameObject item)
    {
        itemPrefab = item;
    }

    //Checks if the item is valid and the player has enought money to buy it
    public void SelectItemToPlace(string itemName)
    {
        if (itemPrices.TryGetValue(itemName, out int cost))
        {
            if (currentMoney >= cost)
            {
                itemCost = cost;
                isPlacingItem = true;
            }
            else
            {
                Debug.Log("Not enough money to buy this item.");
            }
        }
        else
        {
            Debug.Log("Item not found.");
        }
    }

    //Places the current item in the point where the player clicks
    private void PlaceItem()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null)
        {
            Instantiate(itemPrefab, hit.point, Quaternion.identity);
            currentMoney -= itemCost;
            money.GetComponent<Text>().text = "Money: " + currentMoney.ToString();
            isPlacingItem = false;
        }
        else
        {
            Debug.Log("No valid position to place item.");
        }
    }

    //Calls all the InitialiazeButtonsInPanel for all the items
    private void InitializeItemButtons()
    {
        InitializeButtonsInPanel(plantsPanel);
        InitializeButtonsInPanel(animalsPanel);
        InitializeButtonsInPanel(jeepsPanel);
        InitializeButtonsInPanel(terrarianPanel);
    }

    //Puts all the buttons in the panel to the itemButtons dictionary
    private void InitializeButtonsInPanel(GameObject panel)
    {
        foreach (Transform child in panel.transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                string itemName = button.name;
                if (itemPrices.ContainsKey(itemName))
                {
                    itemButtons[itemName] = button;
                }
                else
                {
                    Debug.LogWarning("Item name not found in itemPrices: " + itemName);
                }
            }
            else
            {
                Debug.LogWarning("Button component not found on: " + child.name);
            }
        }
    }

    //Updates the button texts to show the current price of the items
    private void UpdateButtonLabels()
    {
        foreach (var item in itemPrices)
        {
            if (itemButtons.TryGetValue(item.Key, out Button button))
            {
                button.GetComponentInChildren<Text>().text = item.Key + " (" + item.Value + ")";
            }
        }
    }
}