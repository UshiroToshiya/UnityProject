using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    Vector3 defaultPos;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        defaultPos = transform.localPosition;
    }

    public void Shake(float duration, float strength)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(duration, strength));
    }

    IEnumerator ShakeRoutine(float duration, float strength)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            Vector3 offset = Random.insideUnitSphere * strength;
            offset.z = 0f; // 2D / ã‰ºŽ‹“_—p

            transform.localPosition = defaultPos + offset;
            yield return null;
        }

        transform.localPosition = defaultPos;
    }
}
