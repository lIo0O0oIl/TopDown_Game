using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : MonoBehaviour
{
    public Transform PlayerTrm;

    [SerializeField]
    private AIState currentState;

    public UnityEvent<Vector2> OnMovementKeyPress = null;       // 이동방향이 변경되었다 라는 이벤트
    public UnityEvent<Vector2> OnPointerPositionChange = null;      // 내가 바라보는 것이 변경되었다 라는 이벤트

    public UnityEvent OnAttackButtonPress = null;       // 공격키가 눌렸을 때를 말함.

    private void Awake()
    {
        List<AIState> states = transform.Find("AI").GetComponentsInChildren<AIState>().ToList();

        foreach(AIState state in states)
        {
            state.SetUp(transform);
        }
    }

    public void Move(Vector2 moveDirection, Vector2 targetPositoin)
    {
        OnMovementKeyPress?.Invoke(moveDirection.normalized);
        OnPointerPositionChange?.Invoke(targetPositoin);
    }

    public void ChangeState(AIState nexState)
    {
        currentState = nexState;
    }

    private void Update()
    {
        currentState.UpdateState();
    }
}
