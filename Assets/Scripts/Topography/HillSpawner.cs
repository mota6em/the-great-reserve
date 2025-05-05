using UnityEngine;

public class HillSpawner : MonoBehaviour
{
    public GameObject hillPrefab;

    public int hillCount = 1;
    public float startX = -5f;
    public float spacing = 5f;

    private float baseY = 1.5f;
    private float secondRowOffset = 0.25f;
    private float thirdRowOffset = 0.5f;

    void Start()
    {
        for (int i = 0; i < hillCount; i++)
        {
            float x = startX + i * spacing + Random.Range(0.1f, 0.3f);

            // First row (front)
            Vector3 pos1 = new Vector3(x, baseY, 0);
            GameObject hill1 = Instantiate(hillPrefab, pos1, Quaternion.identity);
            hill1.GetComponent<Hill>().SetY(baseY);
            hill1.GetComponent<SpriteRenderer>().sortingOrder = 7;

            // Second row (behind)
            Vector3 pos2 = new Vector3(x+2f, baseY + secondRowOffset, 0);
            GameObject hill2 = Instantiate(hillPrefab, pos2, Quaternion.identity);
            hill2.GetComponent<Hill>().SetY(baseY + secondRowOffset);
            hill2.GetComponent<SpriteRenderer>().sortingOrder = 6;
            // 3rd row (behind)
            Vector3 pos3 = new Vector3(x + 1.5f, baseY + thirdRowOffset, 0);
            GameObject hill3 = Instantiate(hillPrefab, pos3, Quaternion.identity);
            hill3.GetComponent<Hill>().SetY(baseY + thirdRowOffset);
            hill3.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
