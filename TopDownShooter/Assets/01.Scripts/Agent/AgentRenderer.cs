using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRenderer : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FaceDirection(Vector2 pointerInput)     // 마우스가 바라보고 있는 방향에서 위를 기준으로 왼쪽인지 오른쪽인지를 구해서 바꿔준다.
    {
        Vector3 direction = (Vector3)pointerInput - transform.position;
        Vector3 result = Vector3.Cross(Vector2.up, direction);

        spriteRenderer.flipX = result.z > 0;        // 백터의 외적을 비교해서 어디에 있는지를 얻어온다.
        // 또 왼손법칙(유니티, 언리얼에서만)을 기억하자
    }
}
