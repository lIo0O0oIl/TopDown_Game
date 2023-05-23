using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private InitPoolingList poolingListSO;

    private Transform playerTrm;
    public Transform PlayerTrm => playerTrm;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Mulitple GameManager is running");
        }

        Instance = this;

        FindPlayer();

        MakePoolManager();
    }

    private void FindPlayer()
    {
        playerTrm = GameObject.Find("Player").transform;
    }

    private void MakePoolManager()
    {
        PoolManager.Instance = new PoolManager(transform);
        poolingListSO.PoolingPair.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }

    #region 디버그 코드들
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            EnemyBrain b = PoolManager.Instance.Pop("EnemyGrowler") as EnemyBrain;
            b.transform.position = pos;
            b.ShowProgress();       // 진행
        }
    }
    #endregion
}
