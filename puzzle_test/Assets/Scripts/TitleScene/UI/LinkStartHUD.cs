using System.Collections;
using TMPro;
using UnityEngine;

public class LinkStartHUD : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI linkStartText;

    [Header("Timing")]
    public float showDuration = 0.25f;
    public float flickerSpeed = 8f;

    [Header("Scale Effect")]
    public Vector3 startScale = Vector3.one * 1.2f;
    public Vector3 endScale = Vector3.one;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void Play()
    {
        gameObject.SetActive(true);
        StartCoroutine(HUDSequence());
    }

    IEnumerator HUDSequence()
    {
        float time = 0f;

        // 初期化
        transform.localScale = startScale;
        canvasGroup.alpha = 0f;

        // 表示フェーズ
        while (time < showDuration)
        {
            float t = time / showDuration;

            // フェードイン
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            // 微スケール収束
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            // テキスト点滅
            float flicker = Mathf.PingPong(Time.time * flickerSpeed, 1f);
            linkStartText.alpha = flicker;

            time += Time.deltaTime;
            yield return null;
        }

        // 一瞬表示して消す
        yield return new WaitForSeconds(0.05f);

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
