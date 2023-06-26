using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentHealth : MonoBehaviour, IDamageable
{
    public bool IsEnemy => false;
    public Vector3 hitPoint { get; set; }

    [SerializeField]
    private int maxHP;
    private int currentHP;

    public UnityEvent<int> OnInitHealth = null;
    public UnityEvent<int, int> OnHealthChanged = null;

    public int Health
    {
        get => currentHP;
        set
        {
            currentHP = value;
            currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        }
    }

    [SerializeField]
    private bool isDead = false;

    public UnityEvent OnGetHit = null;
    public UnityEvent OnDead = null;

    private void Start()
    {
        currentHP = maxHP;
        OnInitHealth?.Invoke(maxHP);
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    public void AddHealth(int value)
    {
        Health += value;
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    public void GetHit(int damage, Vector3 hitPoint, Vector3 normal)
    {
        if (isDead) return;

        Health -= damage;
        OnGetHit?.Invoke();
        if (Health <= 0)
        {
            OnDead?.Invoke();
            isDead = true;
        }

        OnHealthChanged?.Invoke(currentHP, maxHP);
    }
}
