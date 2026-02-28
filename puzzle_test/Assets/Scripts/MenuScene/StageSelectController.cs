using UnityEngine;

public class StageSelectController : MonoBehaviour
{
    [SerializeField] private MenuStageData stage1;
    [SerializeField] private MenuStageData stage2;
    [SerializeField] private MenuStageData stage3;
    [SerializeField] private MenuStageData stage4;

    [SerializeField] private MenuCameraController cameraController;
    [SerializeField] private GameObject stageSelectRoot;
    [SerializeField] private GameObject characterSelectRoot;

    public void OnStage1Clicked()
    {
        OnStageSelected(stage1.stageId);
    }

    public void OnStage2Clicked()
    {
        OnStageSelected(stage2.stageId);
    }

    public void OnStage3Clicked()
    {
        OnStageSelected(stage3.stageId);
    }

    public void OnStage4Clicked()
    {
        OnStageSelected(stage4.stageId);
    }

    public void OnStageSelected(int stageId)
    {
        GameManager.Instance.SelectStage(stageId);

        stageSelectRoot.SetActive(false);
        characterSelectRoot.SetActive(true);

        cameraController.MoveTo(characterSelectRoot.transform);
    }
}
