using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("カメラ")]
    [Tooltip("複数可")]
    [SerializeField] List<Camera> cameras;
    //[Space]
    [SerializeField] List<SmoothFollowCamera> cameraControllers;

    int currentIndex = 0;
    [Range(0f, 2f)]
    public int selectIndex = 0;

    void Start()
    {
        SwitchCamera(selectIndex);
    }

    void Update()
    {
        if (currentIndex != selectIndex)
            SwitchCamera(selectIndex);
        if (PuzzleGameManager.Instance.isClear)
            SwitchCamera(1);
    }

    public void SwitchCamera(int index)
    {
        if (index < 0 || index >= cameras.Count) return;

        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
        }

        currentIndex = index;
    }

    // 見るターゲットを切り替える
    public void SetLookTarget(Transform target)
    {
        foreach (var cam in cameraControllers)
        {
            cam.lookTarget = target;
            cam.useLookTarget = target != null;
        }
    }

    // 追従対象も切り替え可能（例：プレイヤー変更）
    public void SetFollowTarget(Transform target)
    {
        foreach (var cam in cameraControllers)
        {
            cam.followTarget = target;
        }
    }
}
