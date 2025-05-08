using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;

public class PlantTest
{
    private GameObject plantObject;
    private MockPlant mockPlant;
    private SpriteRenderer spriteRenderer;
    private Texture2D testTexture;

    [SetUp]
    public void SetUp()
    {
        plantObject = new GameObject("TestPlant");
        spriteRenderer = plantObject.AddComponent<SpriteRenderer>();
        mockPlant = plantObject.AddComponent<MockPlant>();

        mockPlant.spriteRenderer = spriteRenderer;

        testTexture = new Texture2D(64, 64);
        mockPlant.defaultSprite = Sprite.Create(testTexture, new Rect(0, 0, 64, 64), Vector2.zero);
        mockPlant.growingSprite = Sprite.Create(testTexture, new Rect(0, 0, 64, 64), Vector2.zero);
    }

    [TearDown]
    public void TearDown()
    {
        if (Application.isPlaying)
        {
            Object.Destroy(plantObject);
            Object.Destroy(testTexture);
        }
        else
        {
            Object.DestroyImmediate(plantObject);
            Object.DestroyImmediate(testTexture);
        }
    }

    [Test]
    public void InitializePlant_SetsCorrectInitialValues()
    {
        // Act
        mockPlant.TestInitializePlant();

        // Assert
        Assert.AreEqual("TestPlant", mockPlant.plantName);
        Assert.AreEqual(true, mockPlant.waterRequirement);
        Assert.AreEqual(true, mockPlant.isReadyToConsume);
        Assert.AreEqual(10f, mockPlant.regrowthInterval);
        Assert.AreEqual(0f, mockPlant.timer);
    }

    [Test]
    public void Consume_SetsCorrectValues()
    {
        // Arrange
        mockPlant.TestInitializePlant();
        spriteRenderer.sprite = mockPlant.defaultSprite;

        // Act
        mockPlant.Consume();

        // Assert
        Assert.AreEqual(0f, mockPlant.timer);
        Assert.AreEqual(false, mockPlant.isReadyToConsume);
        Assert.AreEqual(mockPlant.growingSprite, mockPlant.GetCurrentSprite());
        Assert.AreEqual(new Vector2(0.2f, 0.2f), new Vector2(plantObject.transform.localScale.x, plantObject.transform.localScale.y));
    }

    [UnityTest]
    public IEnumerator CheckGrowth_WhenTimerExceedsRegrowthInterval_ShouldRegrow()
    {
        // Arrange
        mockPlant.TestInitializePlant();
        mockPlant.Consume();

        mockPlant.timer = mockPlant.regrowthInterval + 1f;

        // Act
        mockPlant.TestCheckGrowth();

        yield return null;

        // Assert
        Assert.AreEqual(0f, mockPlant.timer);
        Assert.AreEqual(true, mockPlant.isReadyToConsume);
        Assert.AreEqual(mockPlant.defaultSprite, mockPlant.GetCurrentSprite());
    }

    [UnityTest]
    public IEnumerator CheckGrowth_WhenTimerBelowRegrowthInterval_ShouldNotRegrow()
    {
        // Arrange
        mockPlant.TestInitializePlant();
        mockPlant.Consume();

        mockPlant.timer = mockPlant.regrowthInterval - 1f;
        float initialTimer = mockPlant.timer;

        // Act
        mockPlant.TestCheckGrowth();

        yield return null;

        // Assert
        Assert.Greater(mockPlant.timer, initialTimer); 
        Assert.AreEqual(false, mockPlant.isReadyToConsume);
        Assert.AreEqual(mockPlant.growingSprite, mockPlant.GetCurrentSprite());
    }

    [Test]
    public void ChangeSprite_WithValidSprite_ChangesSprite()
    {
        // Arrange
        Texture2D testTexture2 = new Texture2D(32, 32);
        Sprite testSprite = Sprite.Create(testTexture2, new Rect(0, 0, 32, 32), Vector2.zero);
        Vector2 testSize = new Vector2(0.5f, 0.5f);

        // Act
        mockPlant.TestChangeSprite(testSprite, testSize);

        // Assert
        Assert.AreEqual(testSprite, mockPlant.GetCurrentSprite());
        Assert.AreEqual(testSize.x, plantObject.transform.localScale.x);
        Assert.AreEqual(testSize.y, plantObject.transform.localScale.y);

        // Cleanup
        if (Application.isPlaying)
            Object.Destroy(testTexture2);
        else
            Object.DestroyImmediate(testTexture2);
    }

    [Test]
    public void ChangeSprite_WithNullSprite_DoesNotChangeSprite()
    {
        // Arrange
        mockPlant.TestInitializePlant();
        mockPlant.InitializeSpriteRenderer();

        spriteRenderer.sprite = mockPlant.defaultSprite;
        Sprite initialSprite = mockPlant.GetCurrentSprite();
        Vector3 initialScale = plantObject.transform.localScale;

        LogAssert.Expect(LogType.Error, "ChangeSprite failed: spriteRenderer or newSprite is null on TestPlant");

        // Act
        mockPlant.TestChangeSprite(null, new Vector2(1f, 1f));

        // Assert
        Assert.AreEqual(initialSprite, mockPlant.GetCurrentSprite());
        Assert.AreEqual(initialScale, plantObject.transform.localScale);
    }

    [Test]
    public void Plant_InitialState_HasSpriteRenderer()
    {
        // Assert
        Assert.IsNotNull(spriteRenderer);
        Assert.IsNotNull(mockPlant.GetSpriteRenderer());
    }

    [UnityTest]
    public IEnumerator Consume_ResetGrowthCycle_StartsRegrowthProcess()
    {
        // Arrange
        mockPlant.TestInitializePlant();
        mockPlant.isReadyToConsume = true;
        spriteRenderer.sprite = mockPlant.defaultSprite;

        // Act
        mockPlant.Consume();

        // Fast forward time to just before regrowth
        mockPlant.timer = mockPlant.regrowthInterval - 0.5f;
        mockPlant.TestCheckGrowth();

        yield return null;

        // Assert - Not yet ready
        Assert.IsFalse(mockPlant.isReadyToConsume);

        // Fast forward time to after regrowth
        mockPlant.timer = mockPlant.regrowthInterval + 0.5f;
        mockPlant.TestCheckGrowth();

        yield return null;

        // Assert - Now ready
        Assert.IsTrue(mockPlant.isReadyToConsume);
        Assert.AreEqual(mockPlant.defaultSprite, mockPlant.GetCurrentSprite());
    }

    [Test]
    public void CheckGrowth_NoChangeWhenAlreadyReadyToConsume()
    {
        // Arrange
        mockPlant.TestInitializePlant();
        mockPlant.isReadyToConsume = true;
        spriteRenderer.sprite = mockPlant.defaultSprite;
        mockPlant.timer = 0f;

        // Act
        mockPlant.TestCheckGrowth();

        // Assert
        Assert.Greater(mockPlant.timer, 0f); 
        Assert.IsTrue(mockPlant.isReadyToConsume);
    }
}

// Mock implementation of the abstract Plant class for testing
public class MockPlant : Plant
{
    public new SpriteRenderer spriteRenderer
    {
        get { return base.spriteRenderer; }
        set { base.spriteRenderer = value; }
    }
    public void InitializeSpriteRenderer()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private new void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private new void Update()
    {
        // Override Update to prevent base.Update from being called during tests
    }

    protected override void InitializePlant()
    {
        plantName = "TestPlant";
        waterRequirement = true;
        isReadyToConsume = true;
        regrowthInterval = 10f;
        timer = 0f;
    }

    protected override void CheckGrowth()
    {
        timer += Time.deltaTime;
        if (!isReadyToConsume && timer >= regrowthInterval)
        {
            timer = 0f;
            isReadyToConsume = true;
            ChangeSprite(defaultSprite, new Vector2(0.2f, 0.2f));
        }
    }

    // Test methods that expose protected methods for testing
    public void TestInitializePlant()
    {
        InitializePlant();
    }

    public void TestCheckGrowth()
    {
        CheckGrowth();
    }

    public void TestChangeSprite(Sprite sprite, Vector2 size)
    {
        ChangeSprite(sprite, size);
    }

    // Helper methods for testing
    public Sprite GetCurrentSprite()
    {
        return spriteRenderer.sprite;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
}
