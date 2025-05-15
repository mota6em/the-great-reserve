using UnityEngine;
using TMPro;

public class MessageBoxUI : MonoBehaviour
{
    public static MessageBoxUI Instance;

    public TextMeshProUGUI messageText;
    public GameObject messagePanel;

    private float displayTime = 5f;
    private float timer = 0f;
    private bool showing = false;

    void Awake()
    {
        Instance = this;
        HideMessage();
    }

    void Update()
    {
        if (showing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                HideMessage();
        }
    }

    public void ShowMessage(string msg)
    {
        if (messageText == null || messagePanel == null) return;

        messageText.text = msg;
        messagePanel.SetActive(true);
        timer = displayTime;
        showing = true;
    }

    public void HideMessage()
    {
        messagePanel.SetActive(false);
        showing = false;
    }
}
