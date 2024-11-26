using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 각 맵을 이동하기 위한 트리거 객체
/// OVRGrabbable을 이용해 트리거로 사용
/// </summary>
public class Portal : MonoBehaviour {
    public Transform nextTr; // 이동할 위치
    public string nextBGM; // 이동한 방에 맞는 배경음 이름
    private OVRGrabbable grabbable; // 트리거 역할을 해줌
    private Vector3 s_pos; // 최초 위치
    private Quaternion s_rot; // 최초 각도

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
        // 트리거를 잡은 시점 체크
        if (grabbable.isGrabbed) {
            // 플레이어의 위치 이동
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            player.GetComponent<CharacterController>().enabled = false; // Transform을 이용한 플레이어 이동을 위해 비활성화
            player.position = nextTr.position + Vector3.up;
            player.rotation = nextTr.rotation;
            player.GetComponent<CharacterController>().enabled = true;
            GameManager.Instance.BgSoundPlay(nextBGM);
        } else {
            ResetTransform();
        }
    }

    /// <summary>
    /// 트리거의 위치를 초기화 시켜줌
    /// 플레이어가 트리거를 잡고 이동하면 트리거도 플레이어를 따라가기 때문
    /// </summary>
    void ResetTransform() {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = s_pos;
        transform.rotation = s_rot;
    }
}
