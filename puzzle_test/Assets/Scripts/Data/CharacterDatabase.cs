using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Character Database")]
public class CharacterDatabase : ScriptableObject
{
    public List<CharacterData> characters;

    private Dictionary<int, CharacterData> cache;

    public CharacterData GetById(int id)
    {
        if (cache == null)
        {
            cache = new Dictionary<int, CharacterData>();
            foreach (var c in characters)
            {
                cache[c.characterId] = c;
            }
        }

        return cache.TryGetValue(id, out var data) ? data : null;
    }
}
