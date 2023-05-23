using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public bool IsEnemy => true;
    public Vector3 hitPoint { get; private set; }

    public UnityEvent OnGetHit = null;
    public UnityEvent OnDie = null;

    #region 나중에 SO 로 뺄 영역
    private int maxHealth = 100;
    #endregion

    [SerializeField]
    private int currentHealth = 100;
    private bool isDead = false;

    private EnemyBrain brain;
    private AIActionData aiActionData;

    private void Awake()
    {
        brain = GetComponent<EnemyBrain>();
        aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void GetHit(int damage, Vector3 hitPoint, Vector3 normal)
    {
        if (isDead) return;

        currentHealth -= damage;
        OnGetHit?.Invoke();

        aiActionData.LastSpotPosition = brain.PlayerTrm.position;       // 마지막으로 맞았을 때 위치
        aiActionData.IsArrived = false;

        if (currentHealth <= 0)
        {
            DeadProcess();
        }
    }

    private void DeadProcess()
    {
        isDead = true;
        OnDie?.Invoke();
    }

    public void Init()      // 초기화
    {
        isDead = false;
        currentHealth = maxHealth;
    }
}
