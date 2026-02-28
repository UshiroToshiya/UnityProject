using UnityEngine;

[CreateAssetMenu(menuName = "Game/Skill Data")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public int power;
    public float cooldown;
}
