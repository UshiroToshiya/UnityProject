using UnityEngine;
using static BoardManager;

public class CombatManager : MonoBehaviour
{
    [SerializeField] public EnemyManager enemyManager;
    [SerializeField] private AttackStockManager attackStockManager;

    private Enemy currentEnemy;

    /// <summary>
    /// BoardManager から呼ばれる（即ダメージではない）
    /// </summary>
    public void AddAttackAttribute(AttackAttribute type, int amount)
    {
        if (enemyManager.GetCurrentEnemy() == null) return;
        Debug.Log($"character:{currentEnemy} type]{type} amount:{amount}");
        attackStockManager.Add(type, amount);
        int damage = DamageCalculator.Calculate(
                currentEnemy,
                type,
                amount
            );
        enemyManager.GetCurrentEnemy().TakeDamage(damage);
    }

    /// <summary>
    /// UIボタンから呼ぶ
    /// </summary>
    public void ExecuteAttack()
    {
        if (currentEnemy == null)
        {
            Debug.LogError("Enemy is NULL");
            currentEnemy = enemyManager.GetCurrentEnemy();
        }

        var stocks = attackStockManager.ConsumeAll();

        foreach (var pair in stocks)
        {
            int damage = DamageCalculator.Calculate(
                currentEnemy,
                pair.Key,
                pair.Value
            );

            currentEnemy.TakeDamage(damage);

            Debug.Log($"[Attack] {pair.Key} x{pair.Value} dmg={damage}");
        }
    }
}
