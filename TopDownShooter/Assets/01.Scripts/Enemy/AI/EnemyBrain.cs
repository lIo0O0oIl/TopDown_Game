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

    public UnityEvent<Vector2> OnMovementKeyPress = null;       // 이동방향이 변경되었다 라는 이벤트
    public UnityEvent<Vector2> OnPointerPositionChange = null;      // 내가 바라보는 것이 변경되었다 라는 이벤트

    public UnityEvent OnAttackButtonPress = null;       // 공격키가 눌렸을 때를 말함.

    public UnityEvent OnInit = null;        // 초기화 되었을 때 발생함

    public bool IsActive { get; set; } = false;     // 프로퍼티가 되면서 유니티 이벤트에서 사용이 가능함

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
        initState = currentState;       // 초기값을 저장해두고 init 할 때 되돌려주기
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
        #region 디버그 코드 나중에 지우기
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
        PoolManager.Instance.Push(this);        // 풀로 보내준다.
    }

    public override void Init()
    {
        transform.rotation = Quaternion.identity;   // 회전 원래대로
        PlayerTrm = GameManager.Instance.PlayerTrm;     // 타겟 설정 완료
        IsActive = false;
        bodyCollider.enabled = true;
        agentAnimator.SetAnimationSpeed(1f);
        OnInit?.Invoke();
    }
}
