
using UnityEngine;

public class TouristManager : MonoBehaviour
{
    public int waitingTourists = 0;
    public int minTourists = 1;
    public int maxTourists = 4;
    public ShopScript shopScript;

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
        if (shopScript != null)
        {
            shopScript.AddMoney(requested * 5);
        }
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
            if (jeep != null && jeep.enabled && jeep.gameObject.activeInHierarchy)
                jeep.TryGetTourists();
        }

        foreach (Jeep4 jeep in FindObjectsOfType<Jeep4>())
        {
            if (jeep != null && jeep.enabled && jeep.gameObject.activeInHierarchy)
                jeep.TryGetTourists();
        }
    }
}
