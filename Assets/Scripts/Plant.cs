using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    public string plantName;
    public bool waterRequirement;
    public bool isReadyToConsume;
    public float regrowthInterval;
    public float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializePlant();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrowth();
    }

    protected abstract void InitializePlant();
    protected abstract void CheckGrowth();

}
