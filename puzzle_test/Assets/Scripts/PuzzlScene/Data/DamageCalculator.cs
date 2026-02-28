using UnityEngine;
using static BoardManager;

public static class DamageCalculator
{
    // 旧仕様（残してOK）
    public static int Calculate(Enemy character, int chainCount)
    {
        int baseDamage = chainCount * 10;

        return Mathf.RoundToInt(
            baseDamage
        );
    }

    // ★ 新仕様（属性対応）
    public static int Calculate(
        Enemy character,
        AttackAttribute attribute,
        int amount
    )
    {
        // 基礎ダメージ（幻素量依存）
        int baseDamage = amount * 10;

        // 属性倍率（キャラ or 仮置き）
        //float attrMultiplier = GetAttributeMultiplier(character, attribute);

        float damage =
            baseDamage;

        return Mathf.RoundToInt(damage);
    }

    /*private static float GetAttributeMultiplier(
        CharacterData character,
        AttackAttribute attribute
    )
    {
        // ひとまず等倍（後で拡張）
        return 1.0f;
    }*/
}
