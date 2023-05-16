using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Transform PlayerTrm;

    [SerializeField]
    private AIState currentState;

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
