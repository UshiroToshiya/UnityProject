using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectController : MonoBehaviour
{
    [SerializeField] private CharacterData player1;
    [SerializeField] private CharacterData player2;

    [SerializeField] private MenuCameraController cameraController;
    [SerializeField] private GameObject stageSelectRoot;
    [SerializeField] private GameObject characterSelectRoot;

    public void OnChar1Clicked()
    {
        OnSelect(player1);
    }

    public void OnChar2Clicked()
    {
        OnSelect(player2);
    }

    private void OnSelect(CharacterData menuData)
    {
        Debug.Log($"menuData.characterId: {menuData.characterId}");
        GameManager.Instance.SelectCharacter(menuData.characterId);
        SceneManager.LoadScene("PuzzleScene");
    }

    public void OnBackClicked()
    {
        characterSelectRoot.SetActive(false);
        stageSelectRoot.SetActive(true);

        cameraController.MoveTo(stageSelectRoot.transform);
    }
}
