using UnityEngine;
using System.Collections;

public class RandomDirectionalLight : MonoBehaviour
{
    [Header("References")]
    public Light dirLight;
    public Renderer targetRenderer;

    [Header("Motion Settings")]
    public float changeInterval = 2.0f;   // Œü‚«‚ğ•Ï‚¦‚éŠÔŠu
    public float rotateDuration = 1.0f;   // ‚Ê‚é‚Á‚Æ“®‚­ŠÔ

    private Coroutine rotateCoroutine;

    void Start()
    {
        InvokeRepeating(nameof(ChangeDirection), 0f, changeInterval);
    }

    void ChangeDirection()
    {
        Bounds b = targetRenderer.bounds;

        // ‘ÎÛ”ÍˆÍ“à‚Ìƒ‰ƒ“ƒ_ƒ€ƒ|ƒCƒ“ƒg
        Vector3 randomPoint = new Vector3(
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y),
            Random.Range(b.min.z, b.max.z)
        );

        // –Ú•W‰ñ“]‚ğŒvZ
        Vector3 dir = randomPoint - dirLight.transform.position;
        Quaternion targetRot = Quaternion.LookRotation(dir);

        // “r’†‚Ì‰ñ“]‚ğ~‚ß‚Ä‚©‚çŠJn
        if (rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        rotateCoroutine = StartCoroutine(
            SmoothRotate(targetRot)
        );
    }

    IEnumerator SmoothRotate(Quaternion targetRot)
    {
        Quaternion startRot = dirLight.transform.rotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / rotateDuration;
            dirLight.transform.rotation =
                Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }

        dirLight.transform.rotation = targetRot;
    }
}
