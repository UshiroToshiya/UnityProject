using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeSlotUI : MonoBehaviour
{
    [Header("アイコン　※注意：アルファ値")]
    [SerializeField] Image icon;
    [Header("ゲージ　アルファ値は半分")]
    [SerializeField] Image gaugeFill;
    [SerializeField] Image aura;
    [SerializeField] TextMeshProUGUI levelText;
    float displayGauge;
    float gaugeSpeed = 3f; // ← 調整用

    CharacterAttributeSlot slot;

    public void Bind(CharacterAttributeSlot slotData)
    {
        slot = slotData;
        slot.slotUI = this;

        icon.sprite = slot.icon;
        aura.color = slot.uiColor;
    }

    void Update()
    {
        if (slot == null) return;

        float maxGauge = slot.maxGauge;
        displayGauge = Mathf.Lerp(
           displayGauge,
           slot.currentGauge,
           Time.deltaTime * gaugeSpeed
        );

        gaugeFill.fillAmount = displayGauge / slot.maxGauge;
        gaugeFill.color = slot.uiColor;
        levelText.text = $"Lv {slot.currentLevel}";

        // Lvに応じてオーラ強化
        float intensity = 0.3f + slot.currentLevel * 0.3f;
        aura.color = slot.uiColor * intensity;
        aura.transform.localScale = Vector3.one * (1f + slot.currentLevel * 0.1f);
    }

    public void PlayGaugeBounce(float addScale)
    {
        StartCoroutine(BounceRoutine(gaugeFill.transform.localScale, addScale));
    }

    IEnumerator BounceRoutine(Vector3 baseScale, float addScale)
    {
        Vector3 upScale = baseScale + Vector3.one * addScale;

        float upTime = 0.08f;
        float downTime = 0.12f;
        float t = 0f;

        // 拡大
        while (t < upTime)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(baseScale, upScale, t / upTime);
            yield return null;
        }

        t = 0f;

        // 縮小
        while (t < downTime)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(upScale, baseScale, t / downTime);
            yield return null;
        }

        transform.localScale = baseScale; // 念のため
    }



}
