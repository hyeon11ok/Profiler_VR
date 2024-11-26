using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� ���� �̵��ϱ� ���� Ʈ���� ��ü
/// OVRGrabbable�� �̿��� Ʈ���ŷ� ���
/// </summary>
public class Portal : MonoBehaviour {
    public Transform nextTr; // �̵��� ��ġ
    public string nextBGM; // �̵��� �濡 �´� ����� �̸�
    private OVRGrabbable grabbable; // Ʈ���� ������ ����
    private Vector3 s_pos; // ���� ��ġ
    private Quaternion s_rot; // ���� ����

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
        s_pos = transform.position;
        s_rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Ʈ���Ÿ� ���� ���� üũ
        if (grabbable.isGrabbed) {
            // �÷��̾��� ��ġ �̵�
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            player.GetComponent<CharacterController>().enabled = false; // Transform�� �̿��� �÷��̾� �̵��� ���� ��Ȱ��ȭ
            player.position = nextTr.position + Vector3.up;
            player.rotation = nextTr.rotation;
            player.GetComponent<CharacterController>().enabled = true;
            GameManager.Instance.BgSoundPlay(nextBGM);
        } else {
            ResetTransform();
        }
    }

    /// <summary>
    /// Ʈ������ ��ġ�� �ʱ�ȭ ������
    /// �÷��̾ Ʈ���Ÿ� ��� �̵��ϸ� Ʈ���ŵ� �÷��̾ ���󰡱� ����
    /// </summary>
    void ResetTransform() {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = s_pos;
        transform.rotation = s_rot;
    }
}
