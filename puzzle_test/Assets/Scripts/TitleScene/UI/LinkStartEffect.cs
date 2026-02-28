using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinkStartEffect : MonoBehaviour
{
    public Camera cam;
    public float duration = 2.0f;
    public float moveDistance = 50f;
    public float startFov = 60f;
    public float endFov = 90f;

    public IEnumerator LinkStart()
    {
        float time = 0f;
        Vector3 startPos = cam.transform.position;
        Vector3 endPos = startPos + cam.transform.forward * moveDistance;

        while (time < duration)
        {
            float t = time / duration;

            // 加速感（EaseIn）
            float ease = t * t;

            cam.transform.position = Vector3.Lerp(startPos, endPos, ease);
            cam.fieldOfView = Mathf.Lerp(startFov, endFov, ease);

            time += Time.deltaTime;
            yield return null;
        }

        cam.transform.position = endPos;
        cam.fieldOfView = endFov;

        // 次のシーンへ
        SceneManager.LoadScene("MenuScene");
    }
}
