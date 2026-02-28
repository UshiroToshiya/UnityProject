using System.Collections;
using TMPro;
using UnityEngine;

public class StayTextEffect : MonoBehaviour
{
    public TextMeshProUGUI stayText;

    [Header("Timing")]
    public float showDuration = 0.25f;
    public float flickerSpeed = 8f;
    private bool isPlay = false;
    // Start is called before the first frame update

    public void Play()
    {
        if (isPlay) StartCoroutine(StayText());
    }

    public void ShowText()
    {
        gameObject.SetActive(true);
        isPlay = true;
    }

    public void invisibleText()
    {
        isPlay = false;
        gameObject.SetActive(false);
    }

    IEnumerator StayText()
    {
        // テキスト点滅
        float flicker = Mathf.PingPong(Time.time * flickerSpeed, 1f);
        stayText.alpha = flicker;

        // 一瞬表示して消す
        yield return new WaitForSeconds(0.05f);
    }
}
