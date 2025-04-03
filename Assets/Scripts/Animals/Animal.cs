using TMPro;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    private string animalName;
    private int age;
    private int visionRange;
    private int thirst;
    private int maxHealth;
    private int currentHealth;
    private int hunger;
    protected float moveSpeed;
    private float changeDirectionInterval;
    private float timer;
    private Vector2 targetPosition;
    protected float noiseOffsetX;
    protected float noiseOffsetY;


    public string gameAreaName = "GameArea";
    private Collider2D gameAreaCollider;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameArea = GameObject.Find(gameAreaName);
        if (gameArea != null)
        {
            gameAreaCollider = gameArea.GetComponent<Collider2D>();
            if (gameAreaCollider == null)
            {
                Debug.LogError("The gameArea does not have a Collider2D component.");
                return;
            }
        }
        else
        {
            Debug.LogError("GameArea object not found.");
            return;
        }

        InitializeAnimal();
        SetRandomTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    private void SetRandomTargetPosition()
    {
        if (gameAreaCollider != null)
        {
            Bounds bounds = gameAreaCollider.bounds;
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            targetPosition = new Vector2(randomX, randomY);
        }
    }

    private void MoveTowardsTarget()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    protected abstract void InitializeAnimal();
}