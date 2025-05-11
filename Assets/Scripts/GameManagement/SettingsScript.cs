using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    public GameObject settingsPanel;

    private TimeManager timeManager;
    private bool wasGamePaused = false;

    void Start()
    {
        // Find or create TimeManager
        timeManager = Object.FindFirstObjectByType<TimeManager>();
        if (timeManager == null)
        {
            Debug.LogWarning("TimeManager not found in scene. Time control functionality may be limited.");
        }
    }

    public void ToggleSettings()
    {
        if (settingsPanel != null)
        {
            bool willBeActive = !settingsPanel.activeSelf;
            settingsPanel.SetActive(willBeActive);

            // Pause game when opening settings
            if (willBeActive)
            {
                PauseGame();
            }
            // When closing without using resume button, still resume
            else if (!willBeActive && wasGamePaused)
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        if (timeManager != null)
        {
            timeManager.PauseGame();
            wasGamePaused = true;
        }
        else
        {
            // Fallback if TimeManager doesn't exist
            Time.timeScale = 0f;
            wasGamePaused = true;
        }
    }

    public void ResumeGame()
    {
        if (timeManager != null)
        {
            timeManager.ResumeGame();
            wasGamePaused = false;
        }
        else
        {
            // Fallback if TimeManager doesn't exist
            Time.timeScale = 1f;
            wasGamePaused = false;
        }

        // Close settings panel when resuming
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    // Optional: Method to open time speed adjustment panel
    public void OpenTimeSpeedPanel()
    {
        // This would be connected to your time adjustment button
        // Implement to show a separate panel with time speed controls
    }
}
