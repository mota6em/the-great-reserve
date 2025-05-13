using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class Ranger : People
{
    private float reloadTimer = 0;
    private float range = 7;
    private float timeToReload = 10;
    private bool isLoaded = true;
    private GameObject targetHerbivore = null;
    

    protected override void InitializeCharacter()
    {
        characterName = "Ranger";
        gameObject.tag = "Ranger";
        age = 1;
        shootRange = 0.5f;
        maxThirst = -1;
        currentThirst = -1;
        maxHealth = 100;
        currentHealth = 100;
        maxHunger = -1;
        currentHunger = -1;
        hungerThreshold = -1;

        moveSpeed = 2.0f;
        noiseOffsetX = Random.Range(0f, 100f);
        noiseOffsetY = Random.Range(0f, 100f);
        standTimer = 0f;
        standDuration = 5f;
        timeBetweenStopsTimer = 0f;
        timeBetweenStops = 7f;

    }

    private new void Update()
    {
        base.Update();

        if (isLoaded)
        {
            ShootCarnivore();
        }
        else
        {
            load();
        }
    }


    // reloads his wepon 
    private void load()
    {
        reloadTimer += Time.deltaTime;
        Debug.LogWarning(reloadTimer);
        if (reloadTimer > timeToReload)
        {
            isLoaded = true;
        }
    }

    // Tries to shoot at a Carnivore/Poacher shoots it if he can
    protected void ShootCarnivore()
    {
        GameObject[] Carnivores = GameObject.FindGameObjectsWithTag("Carnivore");
        GameObject[] Poachers = GameObject.FindGameObjectsWithTag("Poacher");   

        if (Carnivores.Length == 0 && Poachers.Length == 0)
        {
            Debug.LogWarning("No Carnivore(s)/Poacher(s) to shoot at in the scene.");
            targetHerbivore = null;
            return;
        }
        else
        {
            Debug.LogWarning("Carnivore(s)/Poacher(s) on the field!");

            GameObject closestCarnivores = null;
            float closestDistance = float.MaxValue;


            foreach (var poacher in Poachers)
            {
                if (TargetInRange(poacher, range))
                {
                    float delay = UnityEngine.Random.Range(1.2f, 4.0f);
                    ShootTarget(poacher);
                }
            }


            foreach (var carnivore in Carnivores)
            {
                if (TargetInRange(carnivore, range))
                {
                    ShootTarget(carnivore);
                }
            }
        }

        
    }

    // shoots the target -> epic instant kill
    private void ShootTarget(GameObject target)
    {
        if (target != null)
        {
            Characters targetComponent = target.GetComponent<Characters>();
            if (targetComponent != null)
            {
                targetComponent.DeleteCharacter();

                Fire();
                PausePerlinNoiseMovement(standDuration);
                Debug.Log($"{characterName} has shot {targetComponent.GetCharacterName()} target.");
            }
            targetHerbivore = null;
        }
    }




    private void Fire()
    {
        reloadTimer = 0;
        isLoaded = false;
    }

    protected override void OnTargetReached() { }
    protected override void HandleHunger() { }
}
