using UnityEngine;

public class TouristManager : MonoBehaviour
{
    public int waitingTourists = 0;
    public int minTourists = 1;
    public int maxTourists = 4;

    public float touristInterval = 5f; // seconds between arrivals
    private float timer = 0f;

    void Start()
    {
        waitingTourists = Random.Range(minTourists, maxTourists + 1);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= touristInterval)
        {
            timer = 0f;
            AddTourists(Random.Range(1, 4)); // 1–3 new tourists
        }
    }

    public void AddTourists(int amount)
    {
        waitingTourists += amount;
        Debug.Log($"Tourists arrived: +{amount}. Total: {waitingTourists}");

        TryWakeJeeps(); 
    }

    public int AssignTourists(int requested)
    {
        int assignable = Mathf.Min(requested, waitingTourists);
        waitingTourists -= assignable;
        return assignable;
    }

    public bool HasTouristsFor(int jeepSize)
    {
        return waitingTourists >= jeepSize;
    }

    private void TryWakeJeeps()
    {
        foreach (Jeep2 jeep in FindObjectsOfType<Jeep2>())
        {
            if (!jeep.enabled && HasTouristsFor(2))
                jeep.enabled = true;
        }

        foreach (Jeep4 jeep in FindObjectsOfType<Jeep4>())
        {
            if (!jeep.enabled && HasTouristsFor(4))
                jeep.enabled = true;
        }
    }
}
