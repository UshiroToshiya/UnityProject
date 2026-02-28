using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider hpSlider;

    [Header("Fill Images")]
    [SerializeField] private Image hpFill;       // 緑
    [SerializeField] private Image damageFill;   // 赤

    [Header("Settings")]
    [SerializeField] private Gradient hpGradient;
    [SerializeField] private float damageDelay = 0.3f;
    [SerializeField] private float damageSpeed = 1.5f;

    private Coroutine damageRoutine;
    private int maxHp;

    // Enemy から呼ぶのはこれだけ
    public void Initialize(int current, int max)
    {
        Debug.Log("hpBar Initialize");
        maxHp = max;

        hpSlider.maxValue = max;
        hpSlider.value = current;

        float ratio = (float)current / max;
        hpFill.color = hpGradient.Evaluate(ratio);

        damageFill.fillAmount = ratio;
    }

    // Enemy から呼ぶのはこれだけ
    public void SetHp(int current)
    {
        Debug.Log($"HpBar.SetHp: {current} / {maxHp}");
        float ratio = (float)current / maxHp;

        hpSlider.value = current;
        //hpFill.fillAmount = ratio;
        hpFill.color = hpGradient.Evaluate(ratio);


        Debug.Log($"HpBar{hpSlider.value}");
        // ★ 非アクティブならコルーチンを使わない
        if (!gameObject.activeInHierarchy)
        {
            damageFill.fillAmount = ratio;
            return;
        }

        if (damageRoutine != null)
            StopCoroutine(damageRoutine);

        damageRoutine = StartCoroutine(DamageAnimation(ratio));
    }

    private IEnumerator DamageAnimation(float target)
    {
        yield return new WaitForSeconds(damageDelay);

        while (damageFill.fillAmount > target)
        {
            damageFill.fillAmount -= Time.deltaTime * damageSpeed;
            yield return null;
        }

        damageFill.fillAmount = target;
    }
}
