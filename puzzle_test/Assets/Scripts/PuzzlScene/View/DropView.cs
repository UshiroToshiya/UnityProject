using System.Collections;
using UnityEngine;

public class DropView : MonoBehaviour
{
    public DropData data;

    [Header("Fall Settings")]
    public float fallSpeed = 5f;   // ★ Inspectorで調整
    public AnimationCurve fallCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    Coroutine fallRoutine;

    [Header("Remove Settings")]
    [SerializeField] private ParticleSystem removeEffect;
    [SerializeField] private AudioSource removeSE;
    [SerializeField] private float removeAnimDuration = 0.3f;


    public void Setup(DropData data)
    {
        this.data = data;
        //Debug.Log($"[DropView.Setup] Visual={gameObject.name} DataType={data.type}");
    }


    // =========================
    // 消去演出
    // =========================
    public void PlayRemoveEffect(System.Action onComplete = null)
    {
        StartCoroutine(RemoveRoutine(onComplete));
    }

    IEnumerator RemoveRoutine(System.Action onComplete)
    {
        // 1️⃣ パーティクル再生
        if (removeEffect != null)
        {
            Quaternion rotation = Quaternion.Euler(30f, 0f, 0f);
            ParticleSystem effect = Instantiate(removeEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
            // Debug.Log($"パーティクル {removeEffect}");
        }

        /*画面を揺らす
         * switch (data.type)
        {
            case DropType.Fire:
                CameraShake.Instance?.Shake(0.12f, 0.12f);
                break;

            case DropType.Thunder:
                CameraShake.Instance?.Shake(0.08f, 0.5f); // 短く強く
                break;

            case DropType.Earth:
                CameraShake.Instance?.Shake(0.18f, 0.15f); // 重い揺れ
                break;
        }*/

        // 2️⃣ SE再生
        if (removeSE != null) removeSE.Play();

        // 3️⃣ スケールで縮むアニメ
        Vector3 startScale = transform.localScale;
        float t = 0f;
        while (t < 10f)
        {
            t += Time.deltaTime / removeAnimDuration;
            float eased = Mathf.SmoothStep(1f, 0f, t);
            transform.localScale = startScale * eased;
            yield return null;
        }

        // 4️⃣ 完全消去
        Destroy(gameObject);

        // 5️⃣ 完了通知
        onComplete?.Invoke();
    }

    // =========================
    // 落下演出
    // =========================
    public void PlayFall(Vector3 targetPos)
    {
        if (fallRoutine != null)
            StopCoroutine(fallRoutine);

        fallRoutine = StartCoroutine(FallRoutine(targetPos));
    }

    IEnumerator FallRoutine(Vector3 targetPos)
    {
        Vector3 start = transform.position;
        float dist = Vector3.Distance(start, targetPos);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * fallSpeed / Mathf.Max(dist, 0.01f);
            float eased = fallCurve.Evaluate(t);
            transform.position = Vector3.Lerp(start, targetPos, eased);
            yield return null;
        }

        transform.position = targetPos;

        // ★ 将来用フック
        OnFallComplete();
    }

    // =========================
    // 将来拡張用
    // =========================
    void OnFallComplete()
    {
        // TODO:
        // ・着地エフェクト
        // ・着地SE
    }

    public void MoveTo(Vector3 target, float duration = 0.1f)
    {
        StartCoroutine(MoveRoutine(target, duration));
    }

    IEnumerator MoveRoutine(Vector3 target, float duration)
    {
        Vector3 start = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.position = target;
    }
}
