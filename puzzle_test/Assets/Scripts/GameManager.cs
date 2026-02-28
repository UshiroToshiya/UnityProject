using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; set; }

    public int SelectedCharacterId { get; set; }
    public int SelectedStageId { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SelectCharacter(int id)
    {
        SelectedCharacterId = id;
    }

    public void SelectStage(int id)
    {
        SelectedStageId = id;
    }
}
