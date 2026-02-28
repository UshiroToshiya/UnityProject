using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private VisualElement stageRoot;
    private VisualElement characterRoot;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        stageRoot = root.Q<VisualElement>("StageSelectRoot");
        characterRoot = root.Q<VisualElement>("CharacterSelectRoot");

        characterRoot.AddToClassList("hidden");

        // ステージボタン
        root.Q<Button>("StageButton1").clicked += () =>
        {
            OnStageSelected(1);
        };

        // キャラボタン
        root.Q<Button>("CharacterButton1").clicked += () =>
        {
            OnCharacterSelected(1);
        };
    }

    void OnStageSelected(int stageId)
    {
        GameManager.Instance.SelectedStageId = stageId;

        stageRoot.AddToClassList("hidden");
        characterRoot.RemoveFromClassList("hidden");
    }

    void OnCharacterSelected(int characterId)
    {
        GameManager.Instance.SelectedCharacterId = characterId;

        UnityEngine.SceneManagement.SceneManager.LoadScene("PuzzleScene");
    }
}
