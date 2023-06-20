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

    private HealthBar healthBar;

    [SerializeField]
    private int currentHealth = 100;
    private bool isDead = false;

    private EnemyBrain brain;
    private AIActionData aiActionData;

    private void Awake()
    {
        brain = GetComponent<EnemyBrain>();
        aiActionData = transform.Find("AI").GetComponent<AIActionData>();
        healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
    }

    public void GetHit(int damage, Vector3 hitPoint, Vector3 normal)
    {
        if (isDead || brain.IsActive == false) return;

        aiActionData.HitPoint = hitPoint;
        aiActionData.HitNormal = normal;

        aiActionData.LastSpotPosition = brain.PlayerTrm.position;       // ���������� �¾��� �� ��ġ
        aiActionData.IsArrived = false;

        currentHealth -= damage;

        OnGetHit?.Invoke();
        if (healthBar.gameObject.activeSelf == false)       // ó������ �ǰݴ��ѰŸ�
        {
            healthBar.gameObject.SetActive(true);
        }

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            DeadProcess();
        }
    }

    private void DeadProcess()
    {
        isDead = true;
        healthBar.gameObject.SetActive(false);
        OnDie?.Invoke();
    }

    public void Init()      // �ʱ�ȭ
    {
        isDead = false;
        currentHealth = maxHealth;

        healthBar.SetHealth(currentHealth);
        healthBar.gameObject.SetActive(false);
    }
}
