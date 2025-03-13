using UnityEngine;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadSaveScene()
    {
        SceneManager.LoadScene("LoadGameScene");
    }

    public void loadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
