using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionData : MonoBehaviour
{
    public Vector3 HitPoint;
    public Vector3 HitNormal;
    public Vector3 LastSpotPosition;        // ������ �߰� ����

    public bool IsAttack;
    public bool IsArrived;

    public void Init()
    {
        LastSpotPosition = transform.position;
        IsArrived = true;
        IsAttack = false;
    }

    private void Start()
    {
        LastSpotPosition = transform.position;
        IsArrived = true;
    }
}
