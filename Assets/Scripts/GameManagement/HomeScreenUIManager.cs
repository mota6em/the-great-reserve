using UnityEngine;

public class HomeScreenUIManager : MonoBehaviour
{
    public GameObject difficultySelectorPanel;

    public void toggleDifficultySelector()
    {
        if (difficultySelectorPanel.activeSelf)
        {
            difficultySelectorPanel.SetActive(false);
        }
        else
        {
            difficultySelectorPanel.SetActive(true);
        }
    }
}
