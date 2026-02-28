using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("名前")]
    public int id;
    public string characterName;
    [Header("バトル関連")]
    public int attack;
    public int maxHp;
    public int defense;

    [Header("パズル補正")]
    public float attackMultiplier = 0.2f;
    [Header("クリティカル率")]
    public float criticalRate = 0.1f;
    [Header("属性")]
    public ElementType elementType;
    [Header("表示")]
    public Sprite icon;
}
