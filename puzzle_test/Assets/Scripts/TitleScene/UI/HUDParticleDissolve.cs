using System.Collections;
using UnityEngine;

public class HUDParticleDissolve : MonoBehaviour
{
    public CanvasGroup hudCanvasGroup;
    public ParticleSystem dissolveParticle;

    public IEnumerator Play()
    {
        yield return new WaitForSeconds(5f);
        // HUDÇë¶è¡Ç∑
        hudCanvasGroup.alpha = 0f;

        // HUDà íuÇ≈ó±éqçƒê∂
        dissolveParticle.transform.position = transform.position;
        dissolveParticle.Play();
    }
}
