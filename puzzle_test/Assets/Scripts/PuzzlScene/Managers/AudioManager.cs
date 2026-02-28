using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;


    [Header("Scene BGM")]
    [SerializeField] private SceneBgmEntry[] sceneBgms;

    [Header("MENU_SFX")]

    [Header("PUZZLE_SFX")]
    [SerializeField] private AudioClip dropSelectSE;
    [SerializeField] private AudioClip moveDorpSE;
    [SerializeField] private AudioClip deleteDropSE;
    [SerializeField] private AudioClip clearSE;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlayDropSelect()
    {
        if (dropSelectSE == null) return;
        sfxSource.PlayOneShot(dropSelectSE);
    }

    public void PlayMoveDrop()
    {
        if (dropSelectSE == null) return;
        sfxSource.PlayOneShot(moveDorpSE);
    }

    public void PlayDeleteDrop()
    {
        if (dropSelectSE == null) return;
        sfxSource.PlayOneShot(dropSelectSE);
    }

    public void PlayClear()
    {
        sfxSource.PlayOneShot(clearSE);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (var entry in sceneBgms)
        {
            if (entry.sceneName == scene.name)
            {
                if (entry.bgm == null)
                {
                    StopBGM();
                }
                else
                {
                    PlayBGM(entry.bgm);
                }
                return;
            }
        }

        // ìoò^Ç≥ÇÍÇƒÇ¢Ç»Ç¢ÉVÅ[ÉìÇÕñ≥âπ
        StopBGM();
    }
    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
        bgmSource.clip = null;
    }
}

[System.Serializable]
public class SceneBgmEntry
{
    public string sceneName;
    public AudioClip bgm;
}
