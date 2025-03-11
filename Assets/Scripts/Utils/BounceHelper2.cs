using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHelper2 : MonoBehaviour
{
    [Header("Scale")]
    public float scaleDuration = 0.2f;
    public float scaleBounce = 1.2f;

    private Vector3 originalScale;
    private bool isBouncing = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isBouncing)
        {
            StartBounce();
        }
    }

    public void StartBounce()
    {
        StartCoroutine(Bounce());
    }

    private IEnumerator Bounce()
    {
        isBouncing = true;
        float halfDuration = scaleDuration / 2f;

        yield return StartCoroutine(ScaleTo(originalScale * scaleBounce, halfDuration));
        yield return StartCoroutine(ScaleTo(originalScale, halfDuration));

        isBouncing = false;
    }

    private IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
