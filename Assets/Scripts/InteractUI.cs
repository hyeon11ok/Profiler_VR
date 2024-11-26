using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 정보 UI 상호작용을 위한 객체
/// OculusIntegration의 UIHelper를 응용하여 UI의 드래그 앤 드롭 구현
/// </summary>
public class InteractUI : MonoBehaviour
{
    private LaserPointer laser; // 레이저 포인트
    private Transform pointer; // 추적을 위한 포인터 정보
    private Vector3 offset; // 오브젝트의 위치와 포인터 사이의 초기 간격

    // Start is called before the first frame update
    void Start()
    {
        laser = GameObject.Find("UIHelpers").transform.GetChild(0).GetComponent<LaserPointer>();
    }

    // 드래그 시작 지점
    // pointer와 offset변수 할당
    public void SetPointer() {
        pointer = laser.cursorVisual.transform;
        offset = pointer.position - transform.position; // UI가 포인터로 클릭한 지점을 유지한 상태로 따라오도록 하기 위함
    }

    // 드래그중 기능
    // UI가 포인터를 따라 움직임
    public void FollowPointer() {
        transform.position = pointer.position - offset; // 최초 클릭 시의 간격을 유지하기 위함
    }

    // 드롭 기능
    // UI는 포인터 추적을 멈추고 추적을 위한 변수 초기화
    public void ResetPointer() {
        pointer = null;
        offset = Vector3.zero;
    }
}
