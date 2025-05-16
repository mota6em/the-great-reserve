using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;

public class AnimalTest
{
    private GameObject animalObject;
    private AnimalTestMockAnimal mockAnimal;
    private GameObject gameAreaObject;
    private BoxCollider2D gameAreaCollider;

    [SetUp]
    public void SetUp()
    {
        gameAreaObject = new GameObject("GameArea");
        gameAreaObject.tag = "GameArea";
        gameAreaCollider = gameAreaObject.AddComponent<BoxCollider2D>();
        gameAreaCollider.size = new Vector2(100, 100);

        animalObject = new GameObject("TestAnimal");
        mockAnimal = animalObject.AddComponent<AnimalTestMockAnimal>();
    }

    [TearDown]
    public void TearDown()
    {
        if (Application.isPlaying)
        {
            Object.Destroy(animalObject);
            Object.Destroy(gameAreaObject);
        }
        else
        {
            Object.DestroyImmediate(animalObject);
            Object.DestroyImmediate(gameAreaObject);
        }
    }

 [Test]
public void InitializeAnimal_SetsCorrectInitialValues()
{
    // Act
    mockAnimal.TestInitializeAnimal();

    // Assert
    Assert.AreEqual("TestAnimal", mockAnimal.GetAnimalName());
    Assert.AreEqual(5, mockAnimal.GetAge());
    Assert.AreEqual(8, mockAnimal.GetVisionRange());
    Assert.AreEqual(100, mockAnimal.GetMaxHealth());
    Assert.AreEqual(100, mockAnimal.GetCurrentHealth());
    Assert.AreEqual(80, mockAnimal.GetMaxHunger());
    Assert.AreEqual(80, mockAnimal.GetCurrentHunger());
    Assert.AreEqual(120, mockAnimal.GetMaxThirst());
    Assert.AreEqual(120, mockAnimal.GetCurrentThirst());
    Assert.AreEqual(3.0f, mockAnimal.GetMoveSpeed());
}

    [UnityTest]
    public IEnumerator DeleteAnimal_ShouldDestroyGameObject()
    {
        // Act
        mockAnimal.DeleteAnimal();

        yield return null;

        // Assert
        Assert.IsTrue(animalObject == null);
    }

    [Test]
    public void MoveToLocation_SetsCorrectTargetPosition()
    {
        // Arrange
        Vector2 targetPos = new Vector2(10, 10);

        // Act
        mockAnimal.TestMoveToLocation(targetPos);

        // Assert
        Assert.AreEqual(targetPos, mockAnimal.GetTargetPosition());
        Assert.IsTrue(mockAnimal.IsMovingToTarget());
    }

    [Test]
    public void PausePerlinNoiseMovement_SetsCorrectState()
    {
        // Arrange
        float testDuration = 5.0f;

        // Act
        mockAnimal.PausePerlinNoiseMovement(testDuration);

        // Assert
        Assert.IsTrue(mockAnimal.IsStanding());
        Assert.AreEqual(testDuration, mockAnimal.GetStandDuration());
        Assert.AreEqual(0f, mockAnimal.GetStandTimer());
    }

    [UnityTest]
    public IEnumerator HandleMovementAndStanding_WhenMovingToTarget_MovesTowardsTarget()
    {
        // Arrange
        mockAnimal.TestInitializeAnimal();
        Vector2 startPos = new Vector2(0, 0);
        Vector2 targetPos = new Vector2(10, 10);
        animalObject.transform.position = startPos;
        mockAnimal.TestMoveToLocation(targetPos);

        // Act
        mockAnimal.TestHandleMovementAndStanding();

        yield return null;

        // Assert
        Vector2 currentPos = animalObject.transform.position;
        Assert.AreNotEqual(startPos, currentPos);

        Vector2 directionVector = (targetPos - startPos).normalized;
        Vector2 movementVector = (currentPos - startPos).normalized;

        float dotProduct = Vector2.Dot(directionVector, movementVector);
        Assert.Greater(dotProduct, 0.9f);
    }

    [UnityTest]
    public IEnumerator HandleMovementAndStanding_WhenStanding_StaysInPlace()
    {
        // Arrange
        mockAnimal.TestInitializeAnimal();
        Vector2 startPos = new Vector2(0, 0);
        animalObject.transform.position = startPos;
        mockAnimal.PausePerlinNoiseMovement(1.0f);

        // Act
        mockAnimal.TestHandleMovementAndStanding();

        yield return null;

        // Assert
        Vector2 currentPos = animalObject.transform.position;
        Assert.AreEqual(startPos, currentPos);
        Assert.IsTrue(mockAnimal.IsStanding());
    }

    [UnityTest]
    public IEnumerator HandleMovementAndStanding_AfterStandDuration_ResumesMoveWithPerlinNoise()
    {
        // Arrange
        mockAnimal.TestInitializeAnimal();
        Vector2 startPos = new Vector2(0, 0);
        animalObject.transform.position = startPos;
        mockAnimal.PausePerlinNoiseMovement(0.1f); 

        mockAnimal.SetStandTimer(0.2f); 
        mockAnimal.TestHandleMovementAndStanding();

        yield return null;

        // Assert
        Assert.IsFalse(mockAnimal.IsStanding());
    }

    [Test]
    public void IsPositionInBoundsTest_WhenOutOfBounds_ReturnsFalse()
    {
        // Arrange
        mockAnimal.TestInitializeAnimal();
        mockAnimal.TestFindGameArea();
        Vector2 positionOutOfBounds = new Vector2(1000, 1000);

        // Act
        bool result = mockAnimal.TestIsPositionInBounds(positionOutOfBounds);

        // Assert
        Assert.IsFalse(result);
    }

    [UnityTest]
    public IEnumerator MoveWithPerlinNoise_GeneratesSmoothRandomMovement()
    {
        // Arrange
        mockAnimal.TestInitializeAnimal();
        Vector2 startPos = new Vector2(0, 0);
        animalObject.transform.position = startPos;

        // Act
        mockAnimal.TestMoveWithPerlinNoise();

        yield return null;

        // Assert
        Vector2 currentPos = animalObject.transform.position;
        Assert.AreNotEqual(startPos, currentPos);
    }

    [Test]
    public void GetterMethods_ReturnCorrectValues()
    {

        // Arrange
        string testName = "TestAnimal";
        int testAge = 3;
        int testVision = 7;
        int testMaxThirst = 90;
        int testCurrentThirst = 80;
        int testMaxHealth = 110;
        int testCurrentHealth = 95;
        int testMaxHunger = 70;
        int testCurrentHunger = 60;
        float testMoveSpeed = 2.5f;

        mockAnimal.SetupMockData(testName, testCurrentHealth, testMaxHealth,
                                testCurrentHunger, testMaxHunger, testCurrentThirst,
                                testMaxThirst, testVision, testAge, testMoveSpeed);

        // Act & Assert
        Assert.AreEqual(testName, mockAnimal.GetAnimalName());
        Assert.AreEqual(testAge, mockAnimal.GetAge());
        Assert.AreEqual(testVision, mockAnimal.GetVisionRange());
        Assert.AreEqual(testMaxThirst, mockAnimal.GetMaxThirst());
        Assert.AreEqual(testCurrentThirst, mockAnimal.GetCurrentThirst());
        Assert.AreEqual(testMaxHealth, mockAnimal.GetMaxHealth());
        Assert.AreEqual(testCurrentHealth, mockAnimal.GetCurrentHealth());
        Assert.AreEqual(testMaxHunger, mockAnimal.GetMaxHunger());
        Assert.AreEqual(testCurrentHunger, mockAnimal.GetCurrentHunger());
        Assert.AreEqual(testMoveSpeed, mockAnimal.GetMoveSpeed());
    }
}

// Mock implementation of the abstract Animal class for testing
public class AnimalTestMockAnimal : Animal
{
    private Collider2D mockGameAreaCollider;

    public bool IsMovingToTarget()
    {
        var field = typeof(Characters).GetField("isMovingToTarget",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);
        return (bool)field.GetValue(this);
    }

    public bool IsStanding()
    {
        var field = typeof(Characters).GetField("isStanding",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);
        return (bool)field.GetValue(this);
    }
    public float GetStandTimer() { return standTimer; }
    public float GetStandDuration() { return standDuration; }
    public Vector2 GetTargetPosition() { return targetPosition; }
    public Collider2D GetGameAreaCollider() { return mockGameAreaCollider; }

    public void SetStandTimer(float value) { standTimer = value; }

    private new void Start()
    {
        // Override Start to prevent base.Start from being called during tests
    }

    protected new void Update()
    {
        // Override Update to prevent base.Update from being called during tests
    }

    protected override void InitializeCharacter()
    {
        characterName = "TestAnimal";
        age = 5;
        visionRange = 8;
        maxThirst = 120;
        currentThirst = 120;
        maxHealth = 100;
        currentHealth = 100;
        maxHunger = 80;
        currentHunger = 80;
        hungerThreshold = 20;

        moveSpeed = 3.0f;
        noiseOffsetX = 0.5f; // Fixed value for testing
        noiseOffsetY = 0.5f; // Fixed value for testing
        standTimer = 0f;
        standDuration = 2f;
        timeBetweenStopsTimer = 0f;
        timeBetweenStops = 5f;
    }

    public override void FindGameArea()
    {
        GameObject gameArea = GameObject.FindGameObjectWithTag("GameArea");
        if (gameArea != null)
        {
            mockGameAreaCollider = gameArea.GetComponent<Collider2D>();
        }
    }
    public void TestInitializeAnimal()
    {
        characterName = null;
        age = 0;
        visionRange = 0;
        maxThirst = 0;
        currentThirst = 0;
        maxHealth = 0;
        currentHealth = 0;
        maxHunger = 0;
        currentHunger = 0;
        hungerThreshold = 0;
        moveSpeed = 0f;

        InitializeCharacter();

        TestFindGameArea();
    }
    public void TestFindGameArea()
    {
        base.FindGameArea();
        GameObject gameArea = GameObject.FindGameObjectWithTag("GameArea");
        if (gameArea != null)
        {
            mockGameAreaCollider = gameArea.GetComponent<Collider2D>();
        }
    }

    public void TestHandleMovementAndStanding()
    {
        HandleMovementAndStanding();
    }

    public void TestMoveWithPerlinNoise()
    {
        MoveWithPerlinNoise();
    }


    public bool TestIsPositionInBounds(Vector2 position)
    {
        if (mockGameAreaCollider == null)
        {
            return true;
        }

        Bounds bounds = mockGameAreaCollider.bounds;
        return position.x > bounds.min.x && position.x < bounds.max.x &&
               position.y > bounds.min.y && position.y < bounds.max.y;
    }

    public void TestMoveToLocation(Vector2 location)
    {
        MoveToLocation(location);
    }

    public void SetupMockData(string name, int health, int maxHealth, int hunger, int maxHunger,
                             int thirst, int maxThirst, int vision, int age, float speed)
    {
        characterName = name;
        currentHealth = health;
        this.maxHealth = maxHealth;
        currentHunger = hunger;
        this.maxHunger = maxHunger;
        currentThirst = thirst;
        this.maxThirst = maxThirst;
        visionRange = vision;
        this.age = age;
        moveSpeed = speed;
    }

    protected override void OnTargetReached() { }
    protected override void HandleHunger() { }
}
