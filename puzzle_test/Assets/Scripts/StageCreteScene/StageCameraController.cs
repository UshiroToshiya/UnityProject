using UnityEngine;

public class StageCameraController : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] Camera targetCamera;

    [Header("角度設定")]
    [SerializeField] float angleX = 45f;
    [SerializeField] float angleY = 45f;

    [Header("距離調整")]
    [SerializeField] float distanceMultiplier = 1.5f;
    [SerializeField] float minDistance = 5f;

    public void AdjustCamera(int width, int depth)
    {
        if (targetCamera == null) return;

        // ステージ中心
        Vector3 center = new Vector3(
            (width - 1) / 2f,
            0f,
            (depth - 1) / 2f
        );

        // ステージサイズから距離算出
        float size = Mathf.Max(width, depth);
        float distance = Mathf.Max(size * distanceMultiplier, minDistance);

        // 回転
        Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);

        // 位置計算
        Vector3 offset = rotation * Vector3.back * distance;

        targetCamera.transform.position = center + offset;
        targetCamera.transform.rotation = rotation;
    }
}
