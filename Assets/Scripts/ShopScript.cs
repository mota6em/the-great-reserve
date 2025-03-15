using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject plantsPanel;
    public GameObject animalsPanel;
    public GameObject jeepsPanel;
    public GameObject terrarianPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
