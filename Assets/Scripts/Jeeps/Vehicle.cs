using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    [HideInInspector] public int id;

    public float speed = 1f;
    [HideInInspector] public int currentIndex = -2;

    public abstract void move();
    public abstract void wait();
    public abstract void TryGetTourists();
}
