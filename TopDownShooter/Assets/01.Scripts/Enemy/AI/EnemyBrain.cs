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

    public bool IsActive = false;

    private EnemyRenderer enemyRenderer;

    private void Awake()
    {
        List<AIState> states = transform.Find("AI").GetComponentsInChildren<AIState>().ToList();

        foreach(AIState state in states)
        {
            state.SetUp(transform);
        }
        enemyRenderer = transform.Find("VisualSprite").GetComponent<EnemyRenderer>();
    }

    public void Move(Vector2 moveDirection, Vector2 targetPositoin)
    {
        OnMovementKeyPress?.Invoke(moveDirection.normalized);
        OnPointerPositionChange?.Invoke(targetPositoin);
    }

    public void Attack(Vector3 targetPos)
    {

    }

    public void ChangeState(AIState nexState)
    {
        currentState = nexState;
    }

    private void Update()
    {
        #region ����� �ڵ� ���߿� �����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            enemyRenderer.ShowProcess(1.5f, () => IsActive = true);
        }
        #endregion
        if (IsActive == false) return;

        currentState.UpdateState();
    }

}
