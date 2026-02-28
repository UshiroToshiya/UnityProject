using System.Collections;
using UnityEngine;

public class ScanLineEffect : MonoBehaviour
{
    [Header("Scan Line")]
    public RectTransform scanLine;
    public CanvasGroup canvasGroup;

    [Header("Scan Settings")]
    public float scanDuration = 0.3f;
    public float startY = 200f;
    public float endY = -200f;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    public void Play()
    {
        StartCoroutine(Scan());
    }

    IEnumerator Scan()
    {
        float time = 0f;

        canvasGroup.alpha = 1f;

        while (time < scanDuration)
        {
            float t = time / scanDuration;

            float y = Mathf.Lerp(startY, endY, t);
            scanLine.anchoredPosition = new Vector2(0, y);

            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}
