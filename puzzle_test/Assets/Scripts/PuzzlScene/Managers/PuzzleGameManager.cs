using UnityEngine;

public class PuzzleGameManager : MonoBehaviour
{
    public static PuzzleGameManager Instance { get; private set; }
    public bool isClear = false;
    //ƒvƒŒƒCƒ„[‚ª‘€ì‚µ‚½‚©”»’è
    public bool CanInput { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OnClear()
    {
        Debug.Log("GAME CLEAR");
        isClear = true;
        CanInput = false;
        AudioManager.Instance.PlayClear();
        PuzzleUIController.Instance.ShowClear();
        PlayerManager.Instance.setCurrentCharacter();
    }
}
