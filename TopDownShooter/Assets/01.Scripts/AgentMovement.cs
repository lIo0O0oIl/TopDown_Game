using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 moveDirection;

    [SerializeField]
    private float speed = 5f, accel = 50f, deAccel = 50f;       // ���ǵ�� �ڿ��������� ���� �׼����̼�

    private float currentSpeed;     // ���� �ӵ�

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
            currentSpeed += accel * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deAccel * Time.deltaTime;
        }

        return Mathf.Clamp(currentSpeed, 0, speed);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = moveDirection * currentSpeed;
    }
}
