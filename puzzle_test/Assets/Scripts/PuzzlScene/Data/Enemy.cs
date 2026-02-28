using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("パラメータ")]
    public int maxHp = 100;
    private int currentHp;

    [Header("UI")]
    [SerializeField] private HpBar hpBar;

    public event Action<int, int> OnHpChanged; // current, max
    public event Action<Enemy> OnDeath;

    public int GetCurrentHp() => currentHp;
    public int GetMaxHp() => maxHp;

    private void Awake()
    {
        // ★ 自分の子オブジェクトから取得
        hpBar = GetComponentInChildren<HpBar>(true);

        if (hpBar == null)
        {
            Debug.LogError("HpBar not found in Enemy children");
        }
    }
    public void Initialize()
    {
        currentHp = maxHp;
        hpBar.gameObject.SetActive(true); // ★ ここ重要
        hpBar.Initialize(currentHp, maxHp);
    }



    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHp);
        hpBar.SetHp(currentHp);
        OnHpChanged?.Invoke(currentHp, maxHp);

        if (currentHp <= 0)
            Die();
    }


    void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
