using System;
using System.Collections;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveDuration = 1.0f;
    public event Action OnMoveCompleted;

    private Coroutine moveCoroutine;

    public void MoveTo(Transform target)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveRoutine(target));
    }

    private IEnumerator MoveRoutine(Transform target)
    {
        Vector3 startPos = cameraTransform.position;
        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, time / moveDuration);

            cameraTransform.position = Vector3.Lerp(startPos, target.position, t);
            yield return null;
        }

        cameraTransform.position = target.position;

        OnMoveCompleted?.Invoke();
    }

}
