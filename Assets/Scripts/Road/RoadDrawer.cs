using UnityEngine;

public class RoadDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private int pointCount = 15;
    [SerializeField] private float zPos = 1f;
    [SerializeField] private float startX = -10f;
      private float endX = 11f;
    float curveHeight = 1.5f;  

    private Vector3[] path;
   

    void Start()
    {
        path = new Vector3[pointCount];
        float step = (endX - startX) / (pointCount - 1);
        float startY = Random.Range(-3f, 3f);
        float endY = Random.Range(-3f, 3f);

        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (float)(pointCount - 1);
            float x = startX + i * step;


            float sine = Mathf.Sin(t * 2 * Mathf.PI);  
            float y = Mathf.Lerp(startY, endY, t) + sine * curveHeight;


            y = Mathf.Clamp(y, -4.5f, 4.5f);  
            path[i] = new Vector3(x, y, zPos);
        }

        lineRenderer.positionCount = path.Length;
        lineRenderer.SetPositions(path);
    }
}

