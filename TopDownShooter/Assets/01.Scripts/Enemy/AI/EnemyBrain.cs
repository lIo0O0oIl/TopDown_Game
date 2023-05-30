using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : PoolableMono
{
    public Transform PlayerTrm;

    [SerializeField]
    private AIState currentState;

    public UnityEvent<Vector2> OnMovementKeyPress = null;       // �̵������� ����Ǿ��� ��� �̺�Ʈ
    public UnityEvent<Vector2> OnPointerPositionChange = null;      // ���� �ٶ󺸴� ���� ����Ǿ��� ��� �̺�Ʈ

    public UnityEvent OnAttackButtonPress = null;       // ����Ű�� ������ ���� ����.

    public UnityEvent OnInit = null;        // �ʱ�ȭ �Ǿ��� �� �߻���

    public bool IsActive { get; set; } = false;     // ������Ƽ�� �Ǹ鼭 ����Ƽ �̺�Ʈ���� ����� ������

    private EnemyRenderer enemyRenderer;
    private EnemyAttack enemyAttack;
    public EnemyAttack EnemyAttackCompo => enemyAttack;

    private CapsuleCollider2D bodyCollider;

    private AIActionData aiActionData;

    private AgentAnimator agentAnimator;
    public AgentAnimator AgentAnimatorCompo => agentAnimator;

    private AIState initState;

    private void Awake()
    {
        List<AIState> states = transform.Find("AI").GetComponentsInChildren<AIState>().ToList();
        aiActionData = transform.Find("AI").GetComponent<AIActionData>();

        foreach (AIState state in states)
        {
            state.SetUp(transform);
        }
        enemyRenderer = transform.Find("VisualSprite").GetComponent<EnemyRenderer>();
        agentAnimator = transform.Find("VisualSprite").GetComponent<AgentAnimator>();
        enemyAttack = GetComponent<EnemyAttack>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        initState = currentState;       // �ʱⰪ�� �����صΰ� init �� �� �ǵ����ֱ�
    }

    public void SetDead()
    {
        IsActive = false;
        bodyCollider.enabled = false;
        StartCoroutine(DealyDissolve(1f));
    }

    private IEnumerator DealyDissolve(float time)
    {
        yield return new WaitForSeconds(time);
        enemyRenderer.StartDissolve(2f);
    }

    public void Move(Vector2 moveDirection, Vector2 targetPositoin)
    {
        OnMovementKeyPress?.Invoke(moveDirection.normalized);
        OnPointerPositionChange?.Invoke(targetPositoin);
    }

    public void Attack(int damage, Vector3 targetPos)
    {
        enemyAttack.Attack(damage, targetPos);
    }

    public void ChangeState(AIState nexState)
    {
        currentState = nexState;
    }

    private void Update()
    {/*
        #region ����� �ڵ� ���߿� �����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            enemyRenderer.ShowProcess(1.5f, () => IsActive = true);
        }
        #endregion*/
        if (IsActive == false) return;

        currentState.UpdateState();
    }

    public void ShowProgress()
    {
        enemyRenderer.ShowProcess(1f, () => {
            IsActive = true;
            currentState = initState;
            aiActionData.Init();
            });
    }

    public void GotoPool()
    {
        PoolManager.Instance.Push(this);        // Ǯ�� �����ش�.
    }

    public override void Init()
    {
        transform.rotation = Quaternion.identity;   // ȸ�� �������
        PlayerTrm = GameManager.Instance.PlayerTrm;     // Ÿ�� ���� �Ϸ�
        IsActive = false;
        bodyCollider.enabled = true;
        agentAnimator.SetAnimationSpeed(1f);
        OnInit?.Invoke();
    }
}
