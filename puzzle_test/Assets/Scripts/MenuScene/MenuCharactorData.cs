using UnityEngine;


[CreateAssetMenu(
    fileName = "Character_",
    menuName = "Game/Menu/Character Data"
)]
public class MenuCharacterData : ScriptableObject
{
    [Header("ID（重複不可）")]
    public int characterId;
    public string characterName;
    public Sprite icon;
    public int attack;
    public int maxHp;
}

