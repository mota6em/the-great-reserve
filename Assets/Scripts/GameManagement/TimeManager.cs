using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Time Speed Settings")]
    [SerializeField] private float defaultTimeScale = 1.0f;
    [SerializeField] private float maxTimeScale = 8.0f;

    private float previousTimeScale;
    private bool isPaused = false;

    void Start()
    {
        SetTimeScale(defaultTimeScale);
    }

    public void SetTimeScale(float scale)
    {
        scale = Mathf.Clamp(scale, 0.1f, maxTimeScale);

        Time.timeScale = scale;
    }

    public void SetGameSpeedPreset(float speedMultiplier)
    {
        if (isPaused)
        {
            isPaused = false;
        }

        SetTimeScale(speedMultiplier);
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = previousTimeScale;
            isPaused = false;
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public float GetCurrentTimeScale()
    {
        return Time.timeScale;
    }

    public float GetPreviousTimeScale()
    {
        return previousTimeScale;
    }
}