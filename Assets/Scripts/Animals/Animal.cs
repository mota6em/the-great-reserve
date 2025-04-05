using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public abstract class Animal : MonoBehaviour
{
    protected string animalName;
    protected int age;
    protected int visionRange;

    //if current thirst is zero, the animal will die
    protected int maxThirst;
    protected int currentThirst;
    protected int maxHealth;
    protected int currentHealth;
    protected int maxHunger;
    protected int currentHunger;
    protected float moveSpeed;
    protected Vector2 targetPosition;
    protected float noiseOffsetX;
    protected float noiseOffsetY;

    private Collider2D gameAreaCollider;

    private bool isMovingToTarget = false;
    private bool isStanding = false;
    protected float standTimer;
    protected float standDuration;
    protected float timeBetweenStopsTimer;
    protected float timeBetweenStops;

    protected float hungerTimer = 0f;
    protected float hungerInterval = 1f;

    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        FindGameArea();
        InitializeAnimal();
        FindUIManager();
    }

    void Update()
    {
        HandleHunger();
        HandleMovementAndStanding();
    }

    private void FindUIManager()
    {
        uiManager = Object.FindFirstObjectByType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene.");
        }
    }
    //Finds the GameArea object in the scene and assigns its Collider2D component to gameAreaCollider
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

    // Handles the movement and standing behavior of the animal
    protected void HandleMovementAndStanding()
    {
        if (isMovingToTarget)
        {
            MoveToLocation(targetPosition);
            //Debug.Log("Moving to target position: " + targetPosition);
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                Debug.Log("Reached target position: " + targetPosition);
                isMovingToTarget = false;
                OnTargetReached();
            }
        }
        else if (isStanding)
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

    // Moves the animal using Perlin noise for smooth movement
    protected void MoveWithPerlinNoise()
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
           //Debug.Log("Moving to new position: " + newPosition);
        }
        else
        {
            direction = -direction;
            newPosition = (Vector2)transform.position + direction * step;
            transform.position = newPosition;
           //Debug.Log("Bounce back from bounds");
        }
    }

    // Checks if the new position is within the bounds of the game area
    private bool IsPositionInBounds(Vector2 position)
    {
        Bounds bounds = gameAreaCollider.bounds;
        return position.x > bounds.min.x && position.x < bounds.max.x &&
               position.y > bounds.min.y && position.y < bounds.max.y;
    }

    //An abstract method that must be implemented by derived classes to initialize the animal's properties

    //Moves the animal to a specified location
    protected void MoveToLocation(Vector2 location)
    {
        targetPosition = location;
        isMovingToTarget = true;
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, location, step);
    }

    private void OnMouseDown()
    {
        if (uiManager != null)
        {
            uiManager.ToggleAnimalDataPanel();
            uiManager.SelectAnimal(this);
        }
    }

    protected abstract void OnTargetReached();
    protected abstract void HandleHunger();
    protected abstract void InitializeAnimal();

    public string GetAnimalName() { return animalName; }
    public int GetAge() { return age; }
    public int GetVisionRange() { return visionRange; }
    public int GetMaxThirst() { return maxThirst; }
    public int GetCurrentThirst() { return currentThirst; }
    public int GetMaxHealth() { return maxHealth; }
    public int GetCurrentHealth() { return currentHealth; }
    public int GetMaxHunger() { return maxHunger; }
    public int GetCurrentHunger() { return currentHunger; }
    public float GetMoveSpeed() { return moveSpeed; }
}