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

    public UnityEvent<Vector2> OnMovementKeyPress = null;       // �̵������� ����Ǿ��� ��� �̺�Ʈ
    public UnityEvent<Vector2> OnPointerPositionChange = null;      // ���� �ٶ󺸴� ���� ����Ǿ��� ��� �̺�Ʈ

    public UnityEvent OnAttackButtonPress = null;       // ����Ű�� ������ ���� ����.

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
