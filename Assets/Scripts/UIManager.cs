using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text animalCountText;
    public Text plantCountText;

    // Update is called once per frame
    void Update()
    {
        UpdateCounts();
    }

    private void UpdateCounts()
    {
        int animalCount = GameObject.FindGameObjectsWithTag("Animal").Length;
        int plantCount = GameObject.FindGameObjectsWithTag("Plant").Length;

        animalCountText.text = "Animals: " + animalCount;
        plantCountText.text = "Plants: " + plantCount;
    }
}
