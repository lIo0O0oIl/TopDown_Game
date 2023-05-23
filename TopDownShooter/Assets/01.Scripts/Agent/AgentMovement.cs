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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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

    public void StopImmediately()
    {
        moveDirection = Vector2.zero;
        currentSpeed = 0;
    }

    private void FixedUpdate()
    {
        OnVelocityChanged?.Invoke(currentSpeed);
        _rigidbody.velocity = moveDirection * currentSpeed;
    }
}
