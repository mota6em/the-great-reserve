using UnityEngine;

public class TopographyEnabler : MonoBehaviour
{
    public GameObject topography;

    void Start()
    {
        if (topography != null)
        {
            topography.SetActive(true);
        }
    }
}
