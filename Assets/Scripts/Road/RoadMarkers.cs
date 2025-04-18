using UnityEngine;
using System.Collections;

public class RoadMarkers : MonoBehaviour
{
    public LineRenderer road;
    public Transform entranceMarker;
    public Transform exitMarker;

    void Start()
    {
        StartCoroutine(SetMarkersAfterRoadGenerated());
    }

    IEnumerator SetMarkersAfterRoadGenerated()
    {
        yield return new WaitForEndOfFrame();

        if (road == null || road.positionCount < 2) yield break;

        Vector3 start = road.GetPosition(0);
        Vector3 end = road.GetPosition(road.positionCount - 1);

        entranceMarker.position = new Vector3(-8f, start.y, 0);
        exitMarker.position = new Vector3(8.5f, end.y-2f, 0);
    }
}
