using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private EnemyManager enemyManager;

    [Header("Stage Data")]
    [SerializeField] private StageDatabase stageDatabase;

    private StageData currentStage;

    void Start()
    {
        LoadStage();
        StartStage();
    }

    /// <summary>
    /// GameManager に保存された StageId から StageData を取得
    /// </summary>
    void LoadStage()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance が存在しません");
            return;
        }

        int stageId = GameManager.Instance.SelectedStageId;
        currentStage = stageDatabase.GetById(stageId);

        if (currentStage == null)
        {
            Debug.LogError($"StageId {stageId} に対応する StageData がありません");
            return;
        }

    }

    /// <summary>
    /// ステージ開始処理（既存ロジックを活かす）
    /// </summary>
    void StartStage()
    {

        StageData stageData = currentStage;

        if (currentStage == null)
        {
            Debug.LogError("StageData がロードされていません");
            return;
        }

        enemyManager.SetEnemyPrefab(stageData.enemyPrefab);

        for (int i = 0; i < currentStage.enemyCount; i++)
        {
            enemyManager.SpawnEnemy();
        }
    }
}
