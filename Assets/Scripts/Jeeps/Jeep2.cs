using UnityEngine;

public class Jeep2 : MonoBehaviour
{
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public int currentIndex = 0;
    [HideInInspector] public float t = 0f;

    public float speed = 1f;
    private float yOffset = 0.3f;
    private bool goingForward = true;

    public Sprite fullJeepSprite;
    public Sprite emptyJeepSprite;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lineRenderer = GameObject.Find("Road").GetComponent<LineRenderer>();
        transform.localScale = new Vector3(0.135f, 0.135f, 1f);

        sr.sprite = fullJeepSprite;
        sr.flipX = false;
    }

    void Update()
    {
        if (lineRenderer == null || lineRenderer.positionCount < 2) return;

        int nextIndex = goingForward ? currentIndex + 1 : currentIndex - 1;
        if (nextIndex < 0 || nextIndex >= lineRenderer.positionCount) return;

        Vector3 start = lineRenderer.GetPosition(currentIndex);
        Vector3 end = lineRenderer.GetPosition(nextIndex);

        t += Time.deltaTime * speed / Vector3.Distance(start, end);
        Vector3 position = Vector3.Lerp(start, end, t);
        position.y += yOffset;
        transform.position = position;
        Vector3 direction = (end - start).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // This keeps smooth turns based on path
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (t >= 1f)
        {
            t = 0f;
            currentIndex = nextIndex;

            // Direction change + sprite switch
            if (goingForward && currentIndex >= lineRenderer.positionCount - 1)
            {
                goingForward = false;
                sr.sprite = emptyJeepSprite;
            }
            else if (!goingForward && currentIndex <= 0)
            {
                goingForward = true;
                sr.sprite = fullJeepSprite;
            }
        }
    }
}