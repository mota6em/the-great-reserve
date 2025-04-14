using UnityEngine;
using System.Collections;

public class JeepTourLogic : MonoBehaviour
{
    public Sprite fullJeepSprite;
    public Sprite emptyJeepSprite;

    private SpriteRenderer sr;
    private Jeep4 jeep;
    private bool isFull = true;
    private bool isWaiting = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        jeep = GetComponent<Jeep4>();
        sr.sprite = fullJeepSprite;
    }

    void Update()
    {
        if (jeep == null || jeep.lineRenderer == null || isWaiting) return;

        // End of path
        if (jeep.currentIndex == jeep.lineRenderer.positionCount - 2 && jeep.t >= 1f)
        {
            StartCoroutine(SwitchToEmptyThenReturn());
        }

        // Start of path
        if (jeep.currentIndex == 0 && jeep.t < 0.01f)
        {
            StartCoroutine(SwitchToFullThenDepart());
        }
    }

    IEnumerator SwitchToEmptyThenReturn()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2f);
        sr.sprite = emptyJeepSprite;
        isFull = false;
        isWaiting = false;
    }

    IEnumerator SwitchToFullThenDepart()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2f);
        sr.sprite = fullJeepSprite;
        isFull = true;
        isWaiting = false;
    }
}
