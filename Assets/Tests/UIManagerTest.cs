using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;


public class UIManagerTests
{
    private UIManager uiManager;
    private GameObject gameArea;

    [SetUp]
    public void SetUp()
    {
        gameArea = new GameObject("GameArea");
        gameArea.tag = "GameArea";
        var collider = gameArea.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(100, 100);

        var uiManagerObject = new GameObject("UIManager");
        uiManager = uiManagerObject.AddComponent<UIManager>();

        uiManager.herbivoreCountText = new GameObject("HerbivoreCountText").AddComponent<Text>();
        uiManager.carnivoreCountText = new GameObject("CarnivoreCountText").AddComponent<Text>();
        uiManager.plantCountText = new GameObject("PlantCountText").AddComponent<Text>();
        uiManager.animalNameText = new GameObject("AnimalNameText").AddComponent<Text>();
        uiManager.animalHealthText = new GameObject("AnimalHealthText").AddComponent<Text>();
        uiManager.animalHungerText = new GameObject("AnimalHungerText").AddComponent<Text>();
        uiManager.animalThirstText = new GameObject("AnimalThirstText").AddComponent<Text>();
        uiManager.animalVisionRangeText = new GameObject("AnimalVisionRangeText").AddComponent<Text>();
        uiManager.animalMovementSpeedText = new GameObject("AnimalMovementSpeedText").AddComponent<Text>();
        uiManager.animalAgeText = new GameObject("AnimalAgeText").AddComponent<Text>();
        uiManager.animalDataPanel = new GameObject("AnimalDataPanel");
    }

    [TearDown]
    public void TearDown()
    {
        if (Application.isPlaying)
        {
            Object.Destroy(uiManager.gameObject);
            Object.Destroy(gameArea);
        }
        else
        {
            Object.DestroyImmediate(uiManager.gameObject);
            Object.DestroyImmediate(gameArea);
        }
    }

    [Test]
    public void SimpleTest()
    {
        Assert.IsTrue(true);
    }

    [UnityTest]
    public IEnumerator SellAnimal_ShouldDeleteAnimalAndClosePanel()
    {
        // Arrange
        var animal = new GameObject().AddComponent<MockAnimal>();
        uiManager.SelectAnimal(animal);

        uiManager.SellAnimal();

        yield return null;

        Assert.That(uiManager.GetSelectedAnimal(), Is.Null);
        Assert.IsFalse(uiManager.animalDataPanel.activeSelf);
    }
    [Test]
    public void SelectAnimal_ShouldSetSelectedAnimal()
    {
        // Arrange
        var animal = new GameObject().AddComponent<MockAnimal>();

        // Act
        uiManager.SelectAnimal(animal);

        // Assert
        Assert.AreEqual(animal, uiManager.GetSelectedAnimal());
    }

    [Test]
    public void OpenAnimalDataPanel_ShouldActivatePanel()
    {
        // Act
        uiManager.OpenAnimalDataPanel();

        // Assert
        Assert.IsTrue(uiManager.animalDataPanel.activeSelf);
    }

    [Test]
    public void CloseAnimalDataPanel_ShouldDeactivatePanel()
    {
        // Act
        uiManager.CloseAnimalDataPanel();

        // Assert
        Assert.IsFalse(uiManager.animalDataPanel.activeSelf);
    }

    [UnityTest]
    public IEnumerator UpdateCounts_ShouldUpdateTextFields()
    {
        // Arrange
        var herbivore = new GameObject { tag = "Herbivore" };
        var carnivore = new GameObject { tag = "Carnivore" };
        var plant = new GameObject { tag = "Plant" };

        // Act
        yield return null;

        var updateCountsMethod = typeof(UIManager).GetMethod("UpdateCounts",
                                 System.Reflection.BindingFlags.NonPublic |
                                 System.Reflection.BindingFlags.Instance);
        updateCountsMethod.Invoke(uiManager, null);

        // Assert
        Assert.AreEqual("Herbivores: 1", uiManager.herbivoreCountText.text);
        Assert.AreEqual("Carnivores: 1", uiManager.carnivoreCountText.text);
        Assert.AreEqual("Plants: 1", uiManager.plantCountText.text);

        // Cleanup
        if (Application.isPlaying)
        {
            Object.Destroy(herbivore);
            Object.Destroy(carnivore);
            Object.Destroy(plant);
        }
        else
        {
            Object.DestroyImmediate(herbivore);
            Object.DestroyImmediate(carnivore);
            Object.DestroyImmediate(plant);
        }
    }

    protected virtual void findGameArea() { }



    [UnityTest]
    public IEnumerator UpdateAnimalInfo_ShouldUpdateAllAnimalTextFields()
    {
        // Arrange
        var animal = new GameObject().AddComponent<MockAnimal>();
        animal.SetupMockData("Tiger", 80, 100, 40, 50, 30, 8, 3.5f);

        // Act
        var updateAnimalInfoMethod = typeof(UIManager).GetMethod("UpdateAnimalInfo",
                                    System.Reflection.BindingFlags.NonPublic |
                                    System.Reflection.BindingFlags.Instance);
        updateAnimalInfoMethod.Invoke(uiManager, new object[] { animal });

        yield return null;

        // Assert
        Assert.AreEqual("Tiger", uiManager.animalNameText.text);
        Assert.AreEqual("Health: 80/100", uiManager.animalHealthText.text);
        Assert.AreEqual("Hunger: 40/50", uiManager.animalHungerText.text);
        Assert.AreEqual("Thirst: 30/30", uiManager.animalThirstText.text);
        Assert.AreEqual("Vision Range: 8", uiManager.animalVisionRangeText.text);
        Assert.AreEqual("Movement Speed: 3,5", uiManager.animalMovementSpeedText.text);
        Assert.AreEqual("Age: 0", uiManager.animalAgeText.text);
    }

    [UnityTest]
    public IEnumerator SellAnimal_WithNoAnimal_ShouldDoNothing()
    {
        // Arrange
        uiManager.CloseAnimalDataPanel();
        uiManager.animalDataPanel.SetActive(true);

        // Act
        uiManager.SellAnimal();
        yield return null;

        // Assert
        Assert.IsTrue(uiManager.animalDataPanel.activeSelf);
    }

    [Test]
    public void GetSelectedAnimal_WhenNoSelection_ShouldReturnNull()
    {
        // Act & Assert
        Assert.IsNull(uiManager.GetSelectedAnimal());
    }

    [Test]
    public void UIManager_InitialState_CountsTextFieldsEmpty()
    {
        // Assert
        Assert.AreEqual(string.Empty, uiManager.herbivoreCountText.text);
        Assert.AreEqual(string.Empty, uiManager.carnivoreCountText.text);
        Assert.AreEqual(string.Empty, uiManager.plantCountText.text);
    }

    [Test]
    public void UIManager_InitialState_AnimalDataPanelInactive()
    {
        // Act
        uiManager.animalDataPanel.SetActive(false);

        // Assert
        Assert.IsFalse(uiManager.animalDataPanel.activeSelf);
    }

}

public class MockAnimal : Animal
{
    private new void Start()
    {
        InitializeCharacter();
    }

    protected new void Update()
    {
    }

    public void SetupMockData(string name, int health, int maxHealth, int hunger, int maxHunger, int thirst, int vision, float speed)
    {
        characterName = name;
        currentHealth = health;
        this.maxHealth = maxHealth;
        currentHunger = hunger;
        this.maxHunger = maxHunger;
        currentThirst = thirst;
        this.maxThirst = thirst;
        visionRange = vision;
        moveSpeed = speed;
    }

    protected override void OnTargetReached() { }
    protected override void HandleHunger() { }
    protected override void InitializeCharacter() { }

    protected new void MoveWithPerlinNoise() { }
    protected new void HandleMovementAndStanding() { }
}
