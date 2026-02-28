using UnityEngine;

[CreateAssetMenu(menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Id")]
    public int characterId;

    [Header("キャラクター")]
    public GameObject player;

}
