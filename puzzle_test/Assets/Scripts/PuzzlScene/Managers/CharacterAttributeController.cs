using System.Collections.Generic;
using UnityEngine;
using static BoardManager;

public class CharacterAttributeController : MonoBehaviour
{
    public List<CharacterAttributeSlot> slots = new();
    public float addScaleGaugeUI = 0.3f;

    public void AddGauge(AttackAttribute attribute, float amount)
    {
        foreach (var slot in slots)
        {
            if (slot.attribute != attribute) continue;
            slot.slotUI.Bind(slot);

            slot.currentGauge += amount;
            // ★ここが「増加した瞬間」
            if (amount > 0)
            {
                slot.slotUI.PlayGaugeBounce(addScaleGaugeUI);
            }
            float gaugeMax = slot.maxGauge;

            if (slot.currentGauge >= gaugeMax &&
                slot.currentLevel < slot.maxLevel)
            {
                slot.currentGauge = 0;
                slot.currentLevel++;
                OnLevelUp(slot);
            }
        }
    }

    void OnLevelUp(CharacterAttributeSlot slot)
    {
        Debug.Log($"{slot.attribute} Lv UP : {slot.currentLevel}");
        // UI・エフェクト通知
    }
}
