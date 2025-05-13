using UnityEngine;

public class CelebrityJeep : Jeep
{
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public float t = 0f;

    public float returnSpeedMultiplier = 3f;
    private float yOffset = 0.3f;
    private bool goingForward = true;

    public Sprite movingSprite;
    public Sprite takingPicturesSprite;

    private SpriteRenderer sr;
    private int touristCount = 0;
    private bool isWaitingForTourists = false;
    private bool hasTourists = false;

    private bool isPaused = false;
    private float pauseDuration = 0f;
    private float pictureChance = 0.15f;
    private float reverseChance = 0.1f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lineRenderer = GameObject.Find("Road").GetComponent<LineRenderer>();
        transform.localScale = new Vector3(0.25f, 0.25f, 1f);

        sr.sprite = movingSprite;
        sr.flipX = false;

        if (lineRenderer != null && lineRenderer.positionCount > 0)
        {
            Vector3 firstPoint = lineRenderer.GetPosition(0);
            Vector3 offsetStart = new Vector3(firstPoint.x - 1f, firstPoint.y, firstPoint.z);
            transform.position = offsetStart;

            Vector3 nextPoint = lineRenderer.GetPosition(1);
            Vector3 direction = (nextPoint - firstPoint).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        TryGetTourists();
    }

    void Update()
    {
        if (isWaitingForTourists || lineRenderer == null || lineRenderer.positionCount < 2)
            return;

        if (isPaused)
        {
            pauseDuration -= Time.deltaTime;
            if (pauseDuration <= 0f)
            {
                sr.sprite = movingSprite;
                isPaused = false;
            }
            return;
        }

        if (Random.value < pictureChance * Time.deltaTime)
        {
            isPaused = true;
            pauseDuration = Random.Range(2f, 4f);
            sr.sprite = takingPicturesSprite;
            return;
        }

        if (Random.value < reverseChance * Time.deltaTime)
        {
            goingForward = !goingForward;
        }

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
                hasTourists = false;
            }
            else if (!goingForward && currentIndex <= 0 && !hasTourists)
            {
                goingForward = true;
                TryGetTourists();

                TouristManager tm = FindObjectOfType<TouristManager>();
                if (tm != null) tm.CelebrityReturned();
            }
        }
    }

    override
    public void TryGetTourists()
    {
        if (hasTourists || currentIndex > 0) return;

        TouristManager tm = FindObjectOfType<TouristManager>();
        if (tm != null)
        {
            touristCount = tm.AssignTourists(1);

            if (touristCount > 0)
            {
                sr.sprite = movingSprite;
                isWaitingForTourists = false;
                hasTourists = true;
            }
            else
            {
                isWaitingForTourists = true;
                hasTourists = false;
                Debug.Log($"{name} is waiting for celebrity tourist...");
            }
        }
    }

    override
    public void wait()
    {
        isWaitingForTourists = true;
    }

    override
    public void move()
    {
        Update();
    }
}
