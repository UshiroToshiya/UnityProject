using UnityEngine;

[CreateAssetMenu(menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("ID（重複不可）")]
    public int stageId;

    public string stageName;

    [Header("Enemy Settings")]
    public int enemyCount;
    public GameObject enemyPrefab;

    [Header("Stage Settings")]
    public int boardWidth;
    public int boardHeight;
}
