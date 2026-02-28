using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Stage Database")]
public class StageDatabase : ScriptableObject
{
    public List<StageData> stages;

    private Dictionary<int, StageData> cache;

    public StageData GetById(int id)
    {
        if (cache == null)
        {
            cache = new Dictionary<int, StageData>();
            foreach (var s in stages)
            {
                cache[s.stageId] = s;
            }
        }

        return cache.TryGetValue(id, out var stage) ? stage : null;
    }
}
