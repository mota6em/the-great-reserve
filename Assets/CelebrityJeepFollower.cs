using UnityEngine;

public class CelebrityJeepFollower : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float speed = 0.5f;

    public Sprite movingSprite;
    public Sprite takingPicturesSprite;

    private SpriteRenderer sr;

    private int currentIndex = 0;
    private float t = 0f;
    private float yOffset = 0.3f;

    private float stopTimer = 0f;
    private float stopInterval = 5f;
    private float stopDuration = 3f;
    private float pauseDuration = 0f;
    private bool isPaused = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (lineRenderer != null && lineRenderer.positionCount > 0)
        {
            Vector3 firstPoint = lineRenderer.GetPosition(0);
            Vector3 startPos = new Vector3(firstPoint.x, firstPoint.y + yOffset, firstPoint.z);
            transform.position = startPos;

            Vector3 nextPoint = lineRenderer.GetPosition(1);
            Vector3 direction = (nextPoint - firstPoint).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        sr.sprite = movingSprite;
    }

    void Update()
    {
        if (lineRenderer == null || lineRenderer.positionCount < 2) return;

        if (isPaused)
        {
            pauseDuration -= Time.deltaTime;
            if (pauseDuration <= 0f)
            {
                isPaused = false;
                sr.sprite = movingSprite;
                Debug.Log(" Finished photo stop");
            }
            return;
        }

        stopTimer += Time.deltaTime;
        if (stopTimer >= stopInterval)
        {
            stopTimer = 0f;
            isPaused = true;
            pauseDuration = stopDuration;
            sr.sprite = takingPicturesSprite;
            Debug.Log("  Celebrity taking photo");
            return;
        }

        Vector3 start = lineRenderer.GetPosition(currentIndex);
        Vector3 end = lineRenderer.GetPosition(currentIndex + 1);

        t += Time.deltaTime * speed / Vector3.Distance(start, end);

        Vector3 position = Vector3.Lerp(start, end, t);
        position.y += yOffset;
        transform.position = position;

        Vector3 direction = (end - start).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (t >= 1f)
        {
            t = 0f;
            currentIndex++;
            if (currentIndex >= lineRenderer.positionCount - 1)
            {
                currentIndex = 0;
                Debug.Log(" Celebrity tour restarted");
            }
        }
    }
}
}
