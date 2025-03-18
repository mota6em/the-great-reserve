using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("LoadGameScene");

    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
