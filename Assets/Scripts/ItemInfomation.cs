using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템을 감지하고 정보를 출력하는 객체
/// </summary>
public class ItemInfomation : MonoBehaviour
{
    public Transform whiteBoard; // 출력된 아이템 정보 오브젝트를 담을 오브젝트
    public GameObject itemInfoUI; // 아이템 정보를 출력할 UI프리펩

    // 정보 오브젝트 풀링용 리스트 변수
    private List<GameObject> activeInfo = new List<GameObject>(); // 활성화된 정보 오브젝트 리스트
    private List<GameObject> unactiveInfo = new List<GameObject>(); // 비활성화된 정보 오브젝트 리스트

    // 아이템 오브젝트 감지 후 정보 오브젝트 풀링
    private void OnTriggerEnter(Collider other) {
        // 충돌체의 태그로 아이템 오브젝트인지 검사
        if (other.gameObject.CompareTag("Item")) {
            if (unactiveInfo.Count == 0) { // 기존에 생성 후 비활성화 된 오브젝트가 없는 경우 오브젝트 생성
                GameObject infoTmp = Instantiate(itemInfoUI, whiteBoard);
                infoTmp.transform.localPosition = Vector3.zero;
                infoTmp.transform.name = other.transform.name;
                infoTmp.GetComponent<Image>().sprite = other.gameObject.GetComponent<Item>().ItemInfo; // 아이템 객체에 담겨있는 이미지 적용
                activeInfo.Add(infoTmp);
            } else { // 기존에 생성 후 비활성화 된 오브젝트가 있는 경우 재활용
                GameObject infoTmp = unactiveInfo[0];
                infoTmp.gameObject.SetActive(true);
                infoTmp.transform.localPosition = Vector3.zero;
                infoTmp.transform.name = other.transform.name;
                infoTmp.GetComponent<Image>().sprite = other.gameObject.GetComponent<Item>().ItemInfo; // 아이템 객체에 담겨있는 이미지 적용
                unactiveInfo.Remove(infoTmp);
                activeInfo.Add(infoTmp);
            }
        }
    }

    // 탐색 범위 밖으로 아이템이 벗어나면 정보 오브젝트 비활성화
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Item")) {
            // 활성화된 오브젝트 리스트 순회하며 대상 오브젝트 탐색
            for (int i = 0; i < activeInfo.Count; i++) {
                // 아이템 이름으로 탐색 후 오브젝트 비활성화
                if (activeInfo[i].name == other.gameObject.GetComponent<Item>().ItemName) { 
                    activeInfo[i].gameObject.SetActive(false);
                    activeInfo.Remove(activeInfo[i]);
                    unactiveInfo.Add(activeInfo[i]);
                    break;
                }
            }
        }
    }
}
