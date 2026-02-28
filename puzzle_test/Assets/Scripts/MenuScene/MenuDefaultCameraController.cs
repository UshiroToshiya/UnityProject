using UnityEngine;

public class MenuDefaultCameraController : MonoBehaviour
{
    [SerializeField] private MenuCameraController cameraController;
    [SerializeField] private Transform stageSelectCameraPoint;
    [SerializeField] private GameObject stageSelectUI;

    void Start()
    {
        cameraController.OnMoveCompleted += ShowStageSelectUI;
        cameraController.MoveTo(stageSelectCameraPoint);
    }

    void ShowStageSelectUI()
    {
        stageSelectUI.SetActive(true);
        cameraController.OnMoveCompleted -= ShowStageSelectUI; // ñYÇÍÇ∏âèú
    }

}
