using UnityEngine;

public class RoadDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private Vector3[] path = new Vector3[]
    {
        new Vector3(-5, 0, 0),
        new Vector3(-3, 1, 0),
        new Vector3(0, 1.5f, 0),
        new Vector3(3, 1, 0),
        new Vector3(5, 0, 0),
    };

    void Start()
    {
        lineRenderer.positionCount = path.Length;
        lineRenderer.SetPositions(path);
    }
}
