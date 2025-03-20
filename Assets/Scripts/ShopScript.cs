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

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            AddMoney(1);
            timer = 0;
        }
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
        terrarianPanel.SetActive(panelName == "terrarians");
    }

    public void AddMoney(int value)
    {
        currentMoney += value;
        money.GetComponent<Text>().text = "Money: "  + currentMoney.ToString();
    }
}
