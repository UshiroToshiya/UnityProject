using System.Collections.Generic;
using System.Linq;
using static BoardManager;

public static class FusionResolver
{
    static readonly Dictionary<string, AttackAttribute> fusionTable =
        new Dictionary<string, AttackAttribute>
    {
        { "Fire+Water", AttackAttribute.Steam },
        { "Fire+Earth", AttackAttribute.Lava },
        { "Water+Earth", AttackAttribute.Wood },
        { "Water+Wind", AttackAttribute.Ice },
        { "Water+Thunder", AttackAttribute.Storm },
        { "Fire+Wind+Thunder", AttackAttribute.Penetration },
    };

    public static AttackAttribute Resolve(HashSet<DropType> types)
    {
        var baseAttrs = types
            .Select(t => (AttackAttribute)t)
            .OrderBy(a => a.ToString())
            .ToArray();

        string key = string.Join("+", baseAttrs);

        if (fusionTable.TryGetValue(key, out var result))
            return result;

        // —Z‡‚È‚µ ¨ ’P‘®«
        return baseAttrs[0];
    }
}
