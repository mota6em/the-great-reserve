using UnityEngine;

public class SafariBackground : MonoBehaviour
{
    public GameObject tilePrefab;
    public int width = 20;
    public int height = 15;
    public float tileSize = 0.5f;

    void Start()
    {
        for (int x = -width / 2; x < width / 2; x++)
        {
            for (int y = -height / 2; y < height / 2; y++)
            {
                Vector3 pos = new Vector3(x * tileSize, y * tileSize, 10f);
                Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
