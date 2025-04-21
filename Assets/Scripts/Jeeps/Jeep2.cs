
using UnityEngine;

public class Jeep2 : MonoBehaviour
{
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public int currentIndex = 0;
    [HideInInspector] public float t = 0f;

    public float speed = 1f;
    public float returnSpeedMultiplier = 3f;
    private float yOffset = 0.3f;
    private bool goingForward = true;

    public Sprite jeep1TouristSprite;
    public Sprite jeep2TouristsSprite;
    public Sprite emptyJeepSprite;
    public Sprite waitingSprite;

    private SpriteRenderer sr;
    private int touristCount = 0;
    private bool isWaitingForTourists = false;
    private bool hasTourists = false;

    /**void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lineRenderer = GameObject.Find("Road").GetComponent<LineRenderer>();
        transform.localScale = new Vector3(0.135f, 0.135f, 1f);

        sr.flipX = false;

        currentIndex = 0;
        transform.position = lineRenderer.GetPosition(0);
        goingForward = true;
        sr.sprite = waitingSprite;

        if (lineRenderer.positionCount >= 2)
        {
            Vector3 dir = (lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1)).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        TryGetTourists();
    }**/
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lineRenderer = GameObject.Find("Road").GetComponent<LineRenderer>();
        transform.localScale = new Vector3(0.135f, 0.135f, 1f);

        sr.sprite = waitingSprite;
        sr.flipX = false;

        if (lineRenderer != null && lineRenderer.positionCount > 0)
        {
            Vector3 firstPoint = lineRenderer.GetPosition(0);
            // Move a bit left on x (off-screen), keep same y and z
            Vector3 offsetStart = new Vector3(firstPoint.x - 1f, firstPoint.y, firstPoint.z);
            transform.position = offsetStart;

            // Face direction to exit (forward along the path)
            Vector3 nextPoint = lineRenderer.GetPosition(1);
            Vector3 direction = (nextPoint - firstPoint).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        TryGetTourists(); // your existing logic
    }

    void Update()
    {
        if (isWaitingForTourists || lineRenderer == null || lineRenderer.positionCount < 2)
            return;

        int nextIndex = goingForward ? currentIndex + 1 : currentIndex - 1;
        if (nextIndex < 0 || nextIndex >= lineRenderer.positionCount) return;

        Vector3 start = lineRenderer.GetPosition(currentIndex);
        Vector3 end = lineRenderer.GetPosition(nextIndex);
        float actualSpeed = speed * (goingForward ? 1f : returnSpeedMultiplier);
        t += Time.deltaTime * actualSpeed / Vector3.Distance(start, end);
        Vector3 position = Vector3.Lerp(start, end, t);
        position.y += yOffset;
        transform.position = position;

        Vector3 direction = (end - start).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (t >= 1f)
        {
            t = 0f;
            currentIndex = nextIndex;

            if (goingForward && currentIndex >= lineRenderer.positionCount - 1)
            {
                goingForward = false;
                sr.sprite = emptyJeepSprite;
                hasTourists = false;
            }
            else if (!goingForward && currentIndex <= 0)
            {
                goingForward = true;
                TryGetTourists();
            }
        }
    }

    public void TryGetTourists()
    {
        if (hasTourists || currentIndex > 0) return;
        TouristManager tm = FindObjectOfType<TouristManager>();
        if (tm != null)
        {
            touristCount = tm.AssignTourists(2);

            if (touristCount > 0)
            {
                SetTouristSprite(touristCount);
                isWaitingForTourists = false;
                hasTourists = true;
            }
            else
            {
                sr.sprite = emptyJeepSprite; 
                isWaitingForTourists = true;
                hasTourists = false;
                Debug.Log($"{name} is waiting for tourists...");
            }
        }
    }

    void SetTouristSprite(int count)
    {
        if (count == 1)
            sr.sprite = jeep1TouristSprite;
        else
            sr.sprite = jeep2TouristsSprite;
    }
}
