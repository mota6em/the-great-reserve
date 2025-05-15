using System.IO;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Sprites;

public class Poacher : People
{
    private float reloadTimer = 0;
    private float range = 7;
    private float timeToReload = 10;
    private bool isLoaded = true;
    private GameObject targetHerbivore = null;

    protected override void InitializeCharacter()
    {
        characterName = "Poacher";
        age = 1;
        shootRange = 0.5f;
        visionRange = 5;
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
        standDuration = 3f;
        timeBetweenStopsTimer = 0f;
        timeBetweenStops = 7f;

        gameObject.tag = "Poacher";
    }

    private void Update()
    {
        base.Update();

        if (isLoaded)
        {
            ShootHerbivore();
        }
        else
        {
            load();
        }

        checkVisibility();
    }


    // If a Poacher is not in a range of a Ranger it hides
    protected void checkVisibility()
    {
        bool rangerNearby = false;
        foreach (var ranger in GameObject.FindGameObjectsWithTag("Ranger"))
        {
            Debug.Log($"Distance to ranger: {Vector3.Distance(transform.position, ranger.transform.position)}");

            if (TargetInRange(ranger, visionRange))
            {
                rangerNearby = true;
                break;
            }
        }

        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
            r.enabled = rangerNearby;    // show if a Ranger is near, hide otherwise
    }


    // reloads his wepon 
    private void load()
    {
        reloadTimer += Time.deltaTime;
        //Debug.LogWarning(reloadTimer);
        if (reloadTimer > timeToReload)
        {
            isLoaded = true;
        }
    }

    // Tries to shoot at a Herbivore shoots it if he can
    protected void ShootHerbivore()
    {
        GameObject[] Herbivores = GameObject.FindGameObjectsWithTag("Herbivore");
        GameObject[] Rangers = GameObject.FindGameObjectsWithTag("Ranger");


        if (Herbivores.Length == 0 && Rangers.Length == 0)
        {
            targetHerbivore = null;
            return;
        }
        else
        {
            Debug.LogWarning("Herbivore(s)/Ranger(s) on the field!");

            GameObject closestCarnivores = null;
            float closestDistance = float.MaxValue;


            foreach (var ranger in Rangers)
            {
                if (TargetInRange(ranger, range))
                {
                    float delay = UnityEngine.Random.Range(1.2f, 4.0f);
                    ShootTarget(ranger);
                }
            }


            foreach (var carnivore in Herbivores)
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
