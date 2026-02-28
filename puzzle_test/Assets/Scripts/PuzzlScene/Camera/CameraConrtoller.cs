using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform boardRoot;
    [Range(0.1f, 1f)]
    public float screenRatio = 0.5f;

    [Header("微調整")]
    [Range(-20f, 20f)] public float offsetXAdjust = 0f;
    [Range(-2f, 2f)] public float offsetZAdjust = 0f;
    [Range(-20f, 20f)] public float offsetYAdjust = 0f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        AdjustCamera();
    }

    public void AdjustCamera()
    {
        if (boardRoot == null) return;
        if (cam == null) cam = GetComponent<Camera>();

        // ===== 盤面のサイズ =====
        // ドロップの数とセルサイズから計算するのが安全
        BoardManager board = boardRoot.GetComponent<BoardManager>();
        if (board == null) return;

        float boardWidth = (board.cols - 1) * board.cellSize;
        float boardHeight = (board.rows - 1) * board.cellSize;

        // ===== 画面比率 =====
        float aspect = (float)Screen.width / Screen.height;

        // 下半分に収めるための距離
        float targetHeight = boardHeight / screenRatio;
        float distance = (targetHeight / 2f) / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);

        // ===== 盤面中心 =====
        Vector3 boardCenter = boardRoot.position + new Vector3(boardWidth / 2f, 0f, -boardHeight / 2f);

        // ===== カメラ配置 =====
        cam.transform.position = boardCenter + new Vector3(offsetXAdjust, distance + offsetYAdjust, distance + offsetZAdjust);
        cam.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void OnValidate()
    {
        if (cam == null) cam = GetComponent<Camera>();
        AdjustCamera();
    }
}
