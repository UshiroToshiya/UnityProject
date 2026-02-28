using UnityEngine;

[CreateAssetMenu(menuName = "Game/Attribute")]
public class AttributeData : ScriptableObject
{
    public string attributeId;          // "fire", "water" ‚È‚Ç
    public Sprite icon;
    public Color uiColor;

    [Header("Gauge")]
    public int maxLevel = 3;
    public float gaugePerDrop = 10f;

    [Header("Effect")]
    public GameObject auraPrefab;
}
