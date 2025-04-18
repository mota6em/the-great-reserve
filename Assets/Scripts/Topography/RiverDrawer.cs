using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RiverDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Material riverMaterial;
    public float textureScrollSpeed = 0.05f;

    [SerializeField] private int pointCount = 40;
    [SerializeField] private float zPos = 0.5f;
    [SerializeField] private float startX = -11f;
    [SerializeField] private float endX = 11f;
    [SerializeField] private float baseY = 4.2f;
    [SerializeField] private float waveHeight = 1f;
    [SerializeField] private float noiseStrength = 0.5f;
    [SerializeField] private float noiseScale = 2f;

    private Vector3[] path;
    private float textureOffsetX = 0f;

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        path = new Vector3[pointCount];
        float step = (endX - startX) / (pointCount - 1);

        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (float)(pointCount - 1);

            float x = startX + i * step;
            x += Mathf.Sin(t * Mathf.PI * 3f) * 0.5f; // horizontal squiggle

            float sine = Mathf.Sin(t * Mathf.PI * 2f) * waveHeight;
            float noise = (Mathf.PerlinNoise(i * noiseScale, 0f) - 0.5f) * noiseStrength;

            float y = baseY + sine + noise;

            path[i] = new Vector3(x, y, zPos);
        }

        lineRenderer.positionCount = path.Length;
        lineRenderer.SetPositions(path);

        // width variation
        AnimationCurve riverWidth = new AnimationCurve(
            new Keyframe(0f, 0.25f),
            new Keyframe(0.5f, 0.28f),
            new Keyframe(1f, 0.25f)
        );
        lineRenderer.widthCurve = riverWidth;

        // material
        if (riverMaterial != null)
        {
            lineRenderer.material = riverMaterial;
            lineRenderer.textureMode = LineTextureMode.Tile;
        }
    }

    void Update()
    {
        // scroll river texture
        if (lineRenderer.material != null)
        {
            textureOffsetX += Time.deltaTime * textureScrollSpeed;
            lineRenderer.material.mainTextureOffset = new Vector2(textureOffsetX, 0f);
        }
    }
}
