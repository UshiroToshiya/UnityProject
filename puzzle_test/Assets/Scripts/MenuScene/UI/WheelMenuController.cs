using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelMenu : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] RectTransform canvasRoot;

    [Header("Items")]
    [SerializeField] List<RectTransform> menuItems = new();

    [Header("Sphere")]
    [SerializeField] float radius = 400f;
    [SerializeField] float tiltAngle = 25f;

    [Header("Input")]
    [SerializeField] float swipeSensitivity = 0.6f;
    [SerializeField] float snapSpeed = 360f;

    float velocity;
    bool isDragging;
    Vector2 lastTouch;

    int itemCount;
    float angleStep;

    public int centerIndex = 0;

    float currentAngle;
    float targetAngle;

    Quaternion initialRotation; // ★ 追加

    [SerializeField] Transform frontCanvasRoot;
    [SerializeField] Transform backCanvasRoot;


    void Start()
    {
        itemCount = menuItems.Count;
        angleStep = 360f / itemCount;

        // ★ Inspector の回転を保持
        initialRotation = transform.localRotation;

        for (int i = 0; i < itemCount; i++)
        {
            float rad = angleStep * i * Mathf.Deg2Rad;
            float y = Mathf.Sin(rad) * radius;
            float z = Mathf.Cos(rad) * radius;

            menuItems[i].anchoredPosition3D = new Vector3(0f, y, z);
        }
    }

    void Update()
    {
        HandleInput();
        ApplyRotation();
        SnapRotation();
        UpdateInteractable();
    }

    // ======================
    // 入力
    // ======================
    void HandleInput()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastTouch;
            targetAngle += delta.y * swipeSensitivity;
            lastTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
#else
    if (Input.touchCount != 1) return;
    Touch t = Input.GetTouch(0);

    if (t.phase == TouchPhase.Began)
    {
        isDragging = true;
        lastTouch = t.position;
    }
    else if (t.phase == TouchPhase.Moved)
    {
        Vector2 delta = t.position - lastTouch;
        targetAngle += delta.y * swipeSensitivity;
        lastTouch = t.position;
    }
    else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
    {
        isDragging = false;
    }
#endif
    }


    // ======================
    // 回転適用（Xのみ）
    // ======================
    void ApplyRotation()
    {
        currentAngle = Mathf.MoveTowards(
            currentAngle,
            targetAngle,
            snapSpeed * Time.deltaTime
        );

        transform.localRotation =
            initialRotation * Quaternion.Euler(currentAngle, 0f, 0f);
    }


    // ======================
    // スナップ
    // ======================
    void SnapRotation()
    {
        if (isDragging) return;

        float snapped = Mathf.Round(targetAngle / angleStep) * angleStep;
        targetAngle = snapped;
    }


    // ======================
    // 中央判定 & クリック制御
    // ======================
    void UpdateInteractable()
    {
        float bestDot = -1f;
        int bestIndex = centerIndex;

        for (int i = 0; i < menuItems.Count; i++)
        {
            Vector3 dir =
                (menuItems[i].position - Camera.main.transform.position).normalized;

            float dot = Vector3.Dot(Camera.main.transform.forward, dir);

            if (dot > bestDot)
            {
                bestDot = dot;
                bestIndex = i;
            }
        }

        centerIndex = WrapIndex(bestIndex - 1);

        for (int i = 0; i < menuItems.Count; i++)
        {
            bool isSelected = (i == centerIndex);

            RectTransform item = menuItems[i];

            // ★ 親Canvasを切り替える
            item.SetParent(
                isSelected ? frontCanvasRoot : backCanvasRoot,
                true
            );

            Button btn = item.GetComponent<Button>();
            if (btn != null)
                btn.interactable = isSelected;
        }
    }


    int WrapIndex(int index)
    {
        int count = menuItems.Count;
        return (index % count + count) % count;
    }


    public int GetSelectedIndex()
    {
        return centerIndex;
    }
}
