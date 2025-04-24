using UnityEngine;
using UnityEngine.UI;


public class TouristUI : MonoBehaviour
{
    public Text touristText;
    private TouristManager tm;

    void Start()
    {
        tm = FindObjectOfType<TouristManager>();
    }

    void Update()
    {
        if (tm != null && touristText != null)
        {
            touristText.text = "Waiting Tourists: " + tm.waitingTourists.ToString();
        }
    }
}
