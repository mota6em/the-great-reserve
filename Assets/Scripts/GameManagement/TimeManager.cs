using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Slider timeSpeedSlider;
    [SerializeField] private Text timeSpeedText;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;

    [Header("Time Speed Settings")]
    [SerializeField] private float minTimeScale = 0.5f;
    [SerializeField] private float maxTimeScale = 3.0f;
    [SerializeField] private float defaultTimeScale = 1.0f;

    private float previousTimeScale;
    private bool isPaused = false;

    void Start()
    {
        // Initialize time scale to default
        SetTimeScale(defaultTimeScale);

        // Setup slider
        if (timeSpeedSlider != null)
        {
            timeSpeedSlider.minValue = minTimeScale;
            timeSpeedSlider.maxValue = maxTimeScale;
            timeSpeedSlider.value = defaultTimeScale;
            timeSpeedSlider.onValueChanged.AddListener(SetTimeScale);
        }

        // Setup pause button
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(PauseGame);
        }

        // Setup play button
        if (playButton != null)
        {
            playButton.onClick.AddListener(ResumeGame);
            playButton.gameObject.SetActive(false); // Hide at start
        }

        UpdateTimeSpeedText();
    }

    public void SetTimeScale(float scale)
    {
        // Ensure scale is within bounds
        scale = Mathf.Clamp(scale, minTimeScale, maxTimeScale);

        // Set the time scale
        Time.timeScale = scale;

        // Update slider if it exists
        if (timeSpeedSlider != null && !Mathf.Approximately(timeSpeedSlider.value, scale))
        {
            timeSpeedSlider.value = scale;
        }

        UpdateTimeSpeedText();
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            isPaused = true;

            if (pauseButton != null)
                pauseButton.gameObject.SetActive(false);

            if (playButton != null)
                playButton.gameObject.SetActive(true);

            UpdateTimeSpeedText();
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = previousTimeScale;
            isPaused = false;

            if (pauseButton != null)
                pauseButton.gameObject.SetActive(true);

            if (playButton != null)
                playButton.gameObject.SetActive(false);

            UpdateTimeSpeedText();
        }
    }

    private void UpdateTimeSpeedText()
    {
        if (timeSpeedText != null)
        {
            if (isPaused)
            {
                timeSpeedText.text = "PAUSED";
            }
            else
            {
                timeSpeedText.text = "Time Speed: " + Time.timeScale.ToString("F1") + "x";
            }
        }
    }
}
