using TMPro;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    protected string animalName;
    protected int age;
    protected int visionRange;
    protected int thirst;
    protected int maxHealth;
    protected int currentHealth;
    protected int hunger;
    protected float moveSpeed;
    private Vector2 targetPosition;
    protected float noiseOffsetX;
    protected float noiseOffsetY;

    private Collider2D gameAreaCollider;

    private bool isStanding = false;
    protected float standTimer;
    protected float standDuration;
    protected float timeBetweenStopsTimer;
    protected float timeBetweenStops;


    // Start is called before the first frame update
    void Start()
    {
        FindGameArea();
        InitializeAnimal();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStanding)
        {
            standTimer += Time.deltaTime;
            if (standTimer >= standDuration)
            {
                isStanding = false;
                standTimer = 0f;
            }
        }
        else
        {
            MoveWithPerlinNoise();
            timeBetweenStopsTimer += Time.deltaTime;
            if (timeBetweenStopsTimer >= timeBetweenStops)
            {
                isStanding = true;
                timeBetweenStopsTimer = 0f;
            }
        }
    }

    private void FindGameArea()
    {
        GameObject gameArea = GameObject.FindGameObjectWithTag("GameArea");
        if (gameArea != null)
        {
            gameAreaCollider = gameArea.GetComponent<Collider2D>();
            if (gameAreaCollider == null)
            {
                Debug.LogError("The gameArea does not have a Collider2D component.");
            }
        }
        else
        {
            Debug.LogError("GameArea object not found.");
        }
    }

    private void MoveWithPerlinNoise()
    {
        float noiseScale = 0.5f;
        float step = moveSpeed * Time.deltaTime;

        float noiseX = Mathf.PerlinNoise(Time.time * 0.5f + noiseOffsetX, 0f) * 2 - 1;
        float noiseY = Mathf.PerlinNoise(0f, Time.time * 0.5f + noiseOffsetY) * 2 - 1;
        Vector2 direction = new Vector2(noiseX, noiseY).normalized * noiseScale;

        Vector2 newPosition = (Vector2)transform.position + direction * step;

        if (IsPositionInBounds(newPosition))
        {
            transform.position = newPosition;
         //   Debug.Log("Moving to new position: " + newPosition);
        }
        else
        {
            direction = -direction;
            newPosition = (Vector2)transform.position + direction * step;
            transform.position = newPosition;
            Debug.Log("Bounce back from bounds");
        }
    }
    private bool IsPositionInBounds(Vector2 position)
    {
        Bounds bounds = gameAreaCollider.bounds;
        return position.x > bounds.min.x && position.x < bounds.max.x &&
               position.y > bounds.min.y && position.y < bounds.max.y;
    }

    public void StandStill()
    {
        isStanding = true;
        standTimer = 0f;
    }
    protected abstract void InitializeAnimal();
}