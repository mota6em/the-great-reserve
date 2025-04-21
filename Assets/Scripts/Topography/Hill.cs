using UnityEngine;

public class Hill : MonoBehaviour
{
    public void SetY(float y)
    {
        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos;
    }
}
