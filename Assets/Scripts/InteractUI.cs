using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���� UI ��ȣ�ۿ��� ���� ��ü
/// OculusIntegration�� UIHelper�� �����Ͽ� UI�� �巡�� �� ��� ����
/// </summary>
public class InteractUI : MonoBehaviour
{
    private LaserPointer laser; // ������ ����Ʈ
    private Transform pointer; // ������ ���� ������ ����
    private Vector3 offset; // ������Ʈ�� ��ġ�� ������ ������ �ʱ� ����

    // Start is called before the first frame update
    void Start()
    {
        laser = GameObject.Find("UIHelpers").transform.GetChild(0).GetComponent<LaserPointer>();
    }

    // �巡�� ���� ����
    // pointer�� offset���� �Ҵ�
    public void SetPointer() {
        pointer = laser.cursorVisual.transform;
        offset = pointer.position - transform.position; // UI�� �����ͷ� Ŭ���� ������ ������ ���·� ��������� �ϱ� ����
    }

    // �巡���� ���
    // UI�� �����͸� ���� ������
    public void FollowPointer() {
        transform.position = pointer.position - offset; // ���� Ŭ�� ���� ������ �����ϱ� ����
    }

    // ��� ���
    // UI�� ������ ������ ���߰� ������ ���� ���� �ʱ�ȭ
    public void ResetPointer() {
        pointer = null;
        offset = Vector3.zero;
    }
}
