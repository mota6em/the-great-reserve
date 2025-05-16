using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenUIManager : MonoBehaviour
{
    public GameObject difficultySelectorPanel;

    public void toggleDifficultySelector()
    {
        difficultySelectorPanel.SetActive(!difficultySelectorPanel.activeSelf);
    }

    public void DifficultySelector(int difficulty)
    {
        PlayerPrefs.SetInt("SelectedDifficulty", difficulty); // 0 = Easy, 1 = Medium, 2 = Hard
    }
}