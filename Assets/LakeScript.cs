using UnityEngine;

public class LakeScript  : MonoBehaviour
{
    public Sprite[] lakeSprites;
    public float frameRate = 0.5f; // seconds between frames

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentFrame = Random.Range(0, lakeSprites.Length); // start randomly
        spriteRenderer.sprite = lakeSprites[currentFrame];
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % lakeSprites.Length;
            spriteRenderer.sprite = lakeSprites[currentFrame];
        }
    }
}
