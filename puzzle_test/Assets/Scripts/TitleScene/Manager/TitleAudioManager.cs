using UnityEngine;

public class TitleAudioManager : MonoBehaviour
{

    public static TitleAudioManager Instance { get; private set; }

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip titleTouchSE;
    [SerializeField] private AudioClip titleTouchLowSE;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }

    public void PlayTouch()
    {
        if (titleTouchSE == null) return;
        sfxSource.volume = 1.0f;
        sfxSource.PlayOneShot(titleTouchSE);
        sfxSource.PlayOneShot(titleTouchLowSE);
    }
}
