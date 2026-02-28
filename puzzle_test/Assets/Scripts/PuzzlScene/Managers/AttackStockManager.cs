using System.Collections.Generic;
using UnityEngine;
using static BoardManager;

public class AttackStockManager : MonoBehaviour
{
    private Dictionary<AttackAttribute, int> stock = new();

    public void Add(AttackAttribute attr, int amount)
    {
        if (!stock.ContainsKey(attr)) stock[attr] = 0;

        stock[attr] += amount;
        // ゲージ加算
        PlayerManager.Instance.playerCharacterAttributeController.AddGauge(attr, 1f); // 1個分
        Debug.Log($"[Stock] {attr} +{amount} → {stock[attr]}");
        // TODO: UI更新
    }

    public Dictionary<AttackAttribute, int> ConsumeAll()
    {
        var result = new Dictionary<AttackAttribute, int>(stock);
        stock.Clear();

        Debug.Log("[Stock] Consume All");
        return result;
    }

    public int GetAmount(AttackAttribute attr)
    {
        return stock.TryGetValue(attr, out var v) ? v : 0;
    }
}
