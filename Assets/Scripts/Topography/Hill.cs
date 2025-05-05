using UnityEngine;

public class Hill : MonoBehaviour
{
    public int height;
    
    public void SetY(float y)
    {
        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos;
    }
    public void affectVisibility() {}
}
