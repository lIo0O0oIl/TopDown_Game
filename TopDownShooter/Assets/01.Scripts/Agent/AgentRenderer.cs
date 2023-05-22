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

    public void FaceDirection(Vector2 pointerInput)     // ���콺�� �ٶ󺸰� �ִ� ���⿡�� ���� �������� �������� ������������ ���ؼ� �ٲ��ش�.
    {
        Vector3 direction = (Vector3)pointerInput - transform.position;
        Vector3 result = Vector3.Cross(Vector2.up, direction);

        spriteRenderer.flipX = result.z > 0;        // ������ ������ ���ؼ� ��� �ִ����� ���´�.
        // �� �޼չ�Ģ(����Ƽ, �𸮾󿡼���)�� �������
    }
}
