using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private List<Enemy> aliveEnemies = new();
    [SerializeField] private Enemy currentEnemy;

    public void SpawnEnemy()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("SpawnPointsが未設定");
            return;
        }

        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject obj = Instantiate(enemyPrefab, spawn.position, spawn.rotation);

        Enemy enemy = obj.GetComponent<Enemy>();
        aliveEnemies.Add(enemy);

        enemy.Initialize();
        SetCurrentEnemy(enemy);

        enemy.OnDeath += HandleEnemyDeath;
    }

    /// <summary>
    /// 現在の攻撃対象を設定
    /// </summary>
    public void SetCurrentEnemy(Enemy enemy)
    {
        currentEnemy = enemy;
    }

    /// <summary>
    /// CombatManagerなどから取得
    /// </summary>
    public Enemy GetCurrentEnemy()
    {
        return currentEnemy;
    }

    void HandleEnemyDeath(Enemy enemy)
    {
        aliveEnemies.Remove(enemy);

        if (enemy == currentEnemy)
        {
            currentEnemy = null;

            // 次の敵を自動でターゲット
            if (aliveEnemies.Count > 0)
            {
                SetCurrentEnemy(aliveEnemies[0]);
            }
        }

        if (aliveEnemies.Count == 0)
        {
            Debug.Log("敵全滅");
            // ★ここでクリア通知
            PuzzleGameManager.Instance.OnClear();
        }
    }

    public void SetEnemyPrefab(GameObject prefab)
    {
        enemyPrefab = prefab;
    }

}
