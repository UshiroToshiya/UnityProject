using UnityEngine;
using static BoardManager;

[System.Serializable]
public class CharacterAttributeSlot
{
    public AttackAttribute attribute;
    public AttributeSlotUI slotUI;
    public int currentLevel = 0;
    public float currentGauge = 0;
    public int maxLevel = 3;
    public int maxGauge = 5;
    public Sprite icon;
    public Color uiColor;

}
