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

    #region ���߿� SO �� �� ����
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

        aiActionData.LastSpotPosition = brain.PlayerTrm.position;       // ���������� �¾��� �� ��ġ
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

    public void Init()      // �ʱ�ȭ
    {
        isDead = false;
        currentHealth = maxHealth;
    }
}
