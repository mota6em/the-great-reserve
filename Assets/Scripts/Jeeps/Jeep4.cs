using UnityEngine;

public class Jeep4 : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float speed = 1f;
    private int currentIndex = 0;
    private float t = 0f;
    private float yOffset = 0.3f;

    void Start()
    {
        lineRenderer = GameObject.Find("Road").GetComponent<LineRenderer>();
        transform.localScale = new Vector3(0.17f, 0.17f, 1f); 
    }

    void Update()
    {
        if (lineRenderer == null || lineRenderer.positionCount < 2) return;

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
                currentIndex = 0;
        }
    }
}
