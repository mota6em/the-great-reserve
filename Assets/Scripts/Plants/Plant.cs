using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    public string plantName;
    public bool waterRequirement;
    public bool isReadyToConsume;
    public float regrowthInterval;
    public float timer;

    protected SpriteRenderer spriteRenderer;
    public Sprite defaultSprite;
    public Sprite growingSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on " + gameObject.name);
        }
        InitializePlant();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrowth();
    }

    protected abstract void InitializePlant();
    protected abstract void CheckGrowth();
    protected void ChangeSprite(Sprite newSprite, Vector2 newSize)
    {
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
            transform.localScale = new Vector3(newSize.x, newSize.y, 1f);
        }
        else
        {
            Debug.LogError("ChangeSprite failed: spriteRenderer or newSprite is null on " + gameObject.name);
        }
    }

    //this is called when something consumes the plant, or you place the plant. It sets the isReadyToConsume to false and changes skin
    public void Consume()
    {
        timer = 0f;
        isReadyToConsume = false;
        ChangeSprite(growingSprite, new Vector2(0.2f, 0.2f));
    }
}