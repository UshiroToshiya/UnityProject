using UnityEngine;

[CreateAssetMenu(menuName = "Game/Menu Stage Data")]
public class MenuStageData : ScriptableObject
{
    public int stageId;
    public string displayName;
    public Sprite icon;
}
