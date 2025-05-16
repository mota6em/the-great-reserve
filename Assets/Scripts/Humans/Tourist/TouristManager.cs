using UnityEngine;

public class TouristManager : MonoBehaviour
{
    public int waitingTourists = 0;
    public int minTourists = 1;
    public int maxTourists = 4;
    public ShopScript shopScript;

    public float touristInterval = 5f;
    private float timer = 0f;

    // Celebrity logic
    public float celebrityInterval = 60f;
    private float celebrityTimer = 0f;
    private bool celebrityOnTour = false;

    void Start()
    {
        waitingTourists = Random.Range(minTourists, maxTourists + 1);
    }

    void Update()
    {
        timer += Time.deltaTime;
        celebrityTimer += Time.deltaTime;

        // Regular tourists
        if (timer >= touristInterval)
        {
            timer = 0f;
            AddTourists(Random.Range(1, 4));
        }

        // Celebrity tourists
        if (!celebrityOnTour && celebrityTimer >= celebrityInterval)
        {
            celebrityTimer = 0f;

            // Check if celebrity jeep is ready
            CelebrityJeep cj = FindObjectOfType<CelebrityJeep>();
            if (cj != null && cj.enabled && cj.gameObject.activeInHierarchy)
            {
                cj.TryGetTourists(); // Will internally check if it can move
                celebrityOnTour = true;
            }
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
            shopScript.AddMoney(assignable * 5);
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

    public void CelebrityReturned()
    {
        celebrityOnTour = false;
    }
}
