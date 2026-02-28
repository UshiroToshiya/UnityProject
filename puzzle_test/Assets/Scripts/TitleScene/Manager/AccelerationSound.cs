using System.Collections;
using UnityEngine;

public class AccelerationSound : MonoBehaviour
{
    public static AccelerationSound Instance { get; private set; }

    [Header("Layers")]
    [SerializeField] AudioSource low;
    [SerializeField] AudioSource mid;
    [SerializeField] AudioSource high;
    [SerializeField] AudioSource finalHit;

    [Header("Pitch")]
    [SerializeField] float lowPitchEnd = 1.1f;
    [SerializeField] float midPitchEnd = 1.4f;
    [SerializeField] float highPitchEnd = 1.8f;

    [Header("Volume")]
    [SerializeField] float lowVol = 0.6f;
    [SerializeField] float midVol = 0.8f;
    [SerializeField] float highVol = 0.7f;

    [SerializeField] float duration = 2.5f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }

    public void Play()
    {
        StartLayer(low, 0.8f, lowVol);
        StartLayer(mid, 1.0f, midVol);
        StartLayer(high, 1.2f, highVol);

        StartCoroutine(Accelerate());
    }

    void StartLayer(AudioSource src, float pitch, float vol)
    {
        src.pitch = pitch;
        src.volume = 0f;
        src.loop = true;
        src.Play();
    }

    IEnumerator Accelerate()
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float n = t / duration;

            low.pitch = Mathf.Lerp(0.8f, lowPitchEnd, n);
            mid.pitch = Mathf.Lerp(1.0f, midPitchEnd, n);
            high.pitch = Mathf.Lerp(1.2f, highPitchEnd, n);

            low.volume = Mathf.Lerp(0f, lowVol, n);
            mid.volume = Mathf.Lerp(0f, midVol, n);
            high.volume = Mathf.Lerp(0f, highVol, n);

            yield return null;
        }
        Debug.Log("ファイナル");
        finalHit.PlayOneShot(finalHit.clip);
    }

    public void StopAll()
    {
        low.Stop();
        mid.Stop();
        high.Stop();
    }
}
