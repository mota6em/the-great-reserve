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
    protected float moveSpeed = 2f;
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

        noiseOffsetX = Random.Range(0f, 100f);
        noiseOffsetY = Random.Range(0f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithPerlinNoise();
    }

    private void MoveWithPerlinNoise()
    {
        float step = moveSpeed * Time.deltaTime;

        // Use Perlin noise to generate smooth random movement
        float noiseX = Mathf.PerlinNoise(Time.time + noiseOffsetX, 0f) * 2 - 1;
        float noiseY = Mathf.PerlinNoise(Time.time + noiseOffsetY, 0f) * 2 - 1;
        Vector2 direction = new Vector2(noiseX, noiseY).normalized;

        Vector2 newPosition = (Vector2)transform.position + direction * step;
       // if (gameAreaCollider.bounds.Contains(newPosition))
       // {
            transform.position = newPosition;
       //     Debug.Log("Moving to new position: " + newPosition);
        //}
        //else
        //{
        //    Debug.Log("New position out of bounds: " + newPosition);
        //}
    }

    protected abstract void InitializeAnimal();
}