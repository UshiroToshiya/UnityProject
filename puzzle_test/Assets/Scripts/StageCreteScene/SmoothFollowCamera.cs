using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    [Header("Follow")]
    public Transform followTarget;
    public Vector3 offset = new Vector3(0, 3, -6);
    public float followSpeed = 10f;

    [Header("Look")]
    public Transform lookTarget;
    public float rotateSpeed = 5f;

    public bool useLookTarget = false;

    void LateUpdate()
    {
        if (followTarget == null) return;

        // à íuí«è]
        Vector3 targetPos = followTarget.position + followTarget.rotation * offset;
        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime
        );

        // å¸Ç´êßå‰
        Vector3 lookPos = useLookTarget && lookTarget != null
            ? lookTarget.position
            : followTarget.position;

        Quaternion targetRot = Quaternion.LookRotation(lookPos - transform.position);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotateSpeed * Time.deltaTime
        );
    }
}
