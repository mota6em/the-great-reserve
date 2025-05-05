using UnityEngine;

[ExecuteInEditMode]
public class TopographyManager : MonoBehaviour
{
    void OnEnable()
    {
#if UNITY_EDITOR
        // Only hide in edit mode (not while playing)
        if (!Application.isPlaying)
            gameObject.SetActive(false);
#endif
    }

    void Update()
    {
        // This ensures re-activation even if Unity skips Start()
        if (Application.isPlaying && !gameObject.activeSelf)
            gameObject.SetActive(true);
    }
}
