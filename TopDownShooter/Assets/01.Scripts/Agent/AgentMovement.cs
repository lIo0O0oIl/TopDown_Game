using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 moveDirection;

    [SerializeField]
    private MovemantDataSO movementData;

    private float currentSpeed;     // ���� �ӵ�

    public UnityEvent<float> OnVelocityChanged = null;

    private bool isKB = false; // ���� �˹� ���ϰ� �ִ°�?
    private Vector2 KBDirection; // �˹�Ǵ� ����
    private float KBTime;
    private float currentKBTime;    // ���� �˹� ����ð�

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public void Movement(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0)     // ���� ���̰� ���� ���� == �Է��� �ް� ���� ����
        {
            if (Vector2.Dot(moveDirection, dir) < 0)    // ���� ����  (�����ִ� �����̶� ���� ������ �ϴ� �����̶� �ݴ� ������ ��)
            {
                currentSpeed = 0;
            }
            moveDirection = dir;
        }

        currentSpeed = CalculateSpeed(dir);
    }

    private float CalculateSpeed(Vector2 moveInput)     // ���� ���� �ӵ��� �������
    {
        if (moveInput.sqrMagnitude > 0)     // ���� ������ ������ �ٵ� ���� �������� �ȳְ� �� �ȿ� ���� ��������. �� moveInput �� ���̰� �ִٸ� ���� ����
        {
            currentSpeed += movementData.Accle * Time.deltaTime;
        }
        else
        {
            currentSpeed -= movementData.DeAccle * Time.deltaTime;
        }

        return Mathf.Clamp(currentSpeed, 0, movementData.MaxSpeed);
    }

    private void CalculateKB()
    {
        currentKBTime += Time.fixedDeltaTime;
        float percent = currentKBTime / KBTime;
        Vector2 moveDir = Vector2.Lerp(KBDirection, moveDirection * currentSpeed, percent);

        moveDirection = moveDir.normalized;     // ����
        currentSpeed = moveDir.magnitude;   // ����

        if (currentKBTime >= KBTime)
        {
            currentKBTime = 0;
            isKB = false;
            StopImmediately();  // �˹� ����
        }
    }

    public void StopImmediately()
    {
        moveDirection = Vector2.zero;
        currentSpeed = 0;
    }

    private void FixedUpdate()
    {
        if (isKB)
        {
            CalculateKB();      // �˹����� ���� �˹���ϴ� �ӵ��� ������ ���ϼ���.
        }
        OnVelocityChanged?.Invoke(currentSpeed);
        _rigidbody.velocity = moveDirection * currentSpeed;
    }

    public void Knockback(Vector2 direction, float time, bool forceMode = false)
    {
        if (enemyBrain.IsActive == true || forceMode == true)        // ��� �ִٸ� �˹����ֱ�, �Ǵ� �׾�� ��尡 Ʈ��� �۵�
        {
            isKB = true;
            KBDirection = direction;
            KBTime = time;
            currentKBTime = 0;
        }
    }
}
