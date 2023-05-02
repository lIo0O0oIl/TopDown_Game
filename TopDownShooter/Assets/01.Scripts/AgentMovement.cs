using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 moveDirection;

    [SerializeField]
    private float speed = 5f, accel = 50f, deAccel = 50f;       // 스피드와 자연스러움을 위한 액셀레이션

    private float currentSpeed;     // 현재 속도

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Movement(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0)     // 현재 길이가 있을 때만 == 입력을 받고 있을 때만
        {
            if (Vector2.Dot(moveDirection, dir) < 0)    // 백터 내적  (가고있는 방향이랑 지금 가려고 하는 방향이랑 반대 방향일 때)
            {
                currentSpeed = 0;
            }
            moveDirection = dir;
        }

        currentSpeed = CalculateSpeed(dir);
    }

    private float CalculateSpeed(Vector2 moveInput)     // 현재 나의 속도를 계산해줌
    {
        if (moveInput.sqrMagnitude > 0)     // 백터 길이의 제곱임 근데 이제 제곱근을 안넣고 그 안에 값만 가져오는. 즉 moveInput 이 길이가 있다면 참인 것임
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
