using System.Collections.Generic;
using UnityEngine;

public class FusionDatabase : MonoBehaviour
{
    public List<FusionRule> rules;

    private Dictionary<string, DropType> lookup;

    void Awake()
    {
        lookup = new Dictionary<string, DropType>();

        foreach (var rule in rules)
        {
            string key = MakeKey(rule.input);
            lookup[key] = rule.output;
        }
    }

    public bool TryFusion(List<DropType> types, out DropType result)
    {
        string key = MakeKey(types);
        return lookup.TryGetValue(key, out result);
    }

    string MakeKey(IEnumerable<DropType> types)
    {
        var list = new List<DropType>(types);
        list.Sort();
        return string.Join("_", list);
    }
}

[System.Serializable]
public class FusionRule
{
    public DropType[] input;   // Å‘å3‚Â
    public DropType output;    // ¶¬‚³‚ê‚éUŒ‚‘®«
}

