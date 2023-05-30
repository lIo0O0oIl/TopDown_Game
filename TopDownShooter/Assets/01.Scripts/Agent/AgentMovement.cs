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

    private float currentSpeed;     // 현재 속도

    public UnityEvent<float> OnVelocityChanged = null;

    private bool isKB = false; // 현재 넉백 당하고 있는가?
    private Vector2 KBDirection; // 넉백되는 방향
    private float KBTime;
    private float currentKBTime;    // 현재 넉백 진행시간

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        enemyBrain = GetComponent<EnemyBrain>();
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

        moveDirection = moveDir.normalized;     // 방향
        currentSpeed = moveDir.magnitude;   // 길이

        if (currentKBTime >= KBTime)
        {
            currentKBTime = 0;
            isKB = false;
            StopImmediately();  // 넉백 종료
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
            CalculateKB();      // 넉백중일 때는 넉백당하는 속도와 방향을 구하세요.
        }
        OnVelocityChanged?.Invoke(currentSpeed);
        _rigidbody.velocity = moveDirection * currentSpeed;
    }

    public void Knockback(Vector2 direction, float time, bool forceMode = false)
    {
        if (enemyBrain.IsActive == true || forceMode == true)        // 살아 있다면 넉백해주기, 또는 죽었어도 모드가 트루면 작동
        {
            isKB = true;
            KBDirection = direction;
            KBTime = time;
            currentKBTime = 0;
        }
    }
}
