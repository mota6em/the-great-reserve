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
    private int moveSpeed;
    private float changeDirectionInterval;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
    }

    protected abstract void CheckStatus();
    /*
     * timer += Time.deltaTime;
        if (timer >= changeDirectionInterval)
        {
            SetRandomTargetPosition();
            timer = 0;
        }

        SetRandomTargetPosition();
     */


    protected abstract void SetRandomTargetPosition();
}