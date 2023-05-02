using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementKeyPressed = null;
    public UnityEvent<Vector2> OnPointerPositionChanged = null;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        GetKeyInput();
        GetPointerInput();
    }

    private void GetPointerInput()
    {
        // ��ũ�� ��ǥ, ���� ��ǥ, ����Ʈ��ǥ
        Vector3 mousePos = Input.mousePosition;     // ��ũ�� �������� ���콺 ��ǥ�� �˾ƿ�
        mousePos.z = 0;
        Vector2 mouseInWorldPos = mainCam.ScreenToWorldPoint(mousePos);     // ���� ��ǥ�� ��������

        OnPointerPositionChanged?.Invoke(mouseInWorldPos);
    }

    private void GetKeyInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        // Raw�� �ִ� ������ Ű����� �Է��� �޴� ����

        Vector2 dir = new Vector2(x, y);

        OnMovementKeyPressed?.Invoke(dir.normalized);
        // ��ֶ������� ���̸� 1�� ���ְ� ���⸸ ������
    }
}
