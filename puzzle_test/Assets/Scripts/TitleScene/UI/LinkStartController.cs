using UnityEngine;

public class LinkStartController : MonoBehaviour
{
    public LinkStartEffect linkStartEffect;
    [SerializeField] LinkStartHUD hud;
    [SerializeField] ScanLineEffect scanLine;
    [SerializeField] StayTextEffect stayText;
    [SerializeField] HUDParticleDissolve hudDissolve;
    public float startTime = 3f;
    private bool isStarted = false;

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;

        if (startTime <= 0)
        {
            startTitle();
        }
    }

    public void startTitle()
    {
        if (isStarted) return;

        if (Input.GetMouseButtonDown(0))
        {
            isStarted = true;
            OnTitleTouched();
        }
        else
        {
            stayText.ShowText();
            stayText.Play();
        }
    }



    public void OnTitleTouched()
    {
        AccelerationSound.Instance.Play();
        stayText.invisibleText();
        StartCoroutine(linkStartEffect.LinkStart());
        hud.Play();
        scanLine.Play();
        TitleAudioManager.Instance.PlayTouch();

        StartCoroutine(hudDissolve.Play());
    }
}
