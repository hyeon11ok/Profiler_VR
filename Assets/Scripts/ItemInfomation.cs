using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������� �����ϰ� ������ ����ϴ� ��ü
/// </summary>
public class ItemInfomation : MonoBehaviour
{
    public Transform whiteBoard; // ��µ� ������ ���� ������Ʈ�� ���� ������Ʈ
    public GameObject itemInfoUI; // ������ ������ ����� UI������

    // ���� ������Ʈ Ǯ���� ����Ʈ ����
    private List<GameObject> activeInfo = new List<GameObject>(); // Ȱ��ȭ�� ���� ������Ʈ ����Ʈ
    private List<GameObject> unactiveInfo = new List<GameObject>(); // ��Ȱ��ȭ�� ���� ������Ʈ ����Ʈ

    // ������ ������Ʈ ���� �� ���� ������Ʈ Ǯ��
    private void OnTriggerEnter(Collider other) {
        // �浹ü�� �±׷� ������ ������Ʈ���� �˻�
        if (other.gameObject.CompareTag("Item")) {
            if (unactiveInfo.Count == 0) { // ������ ���� �� ��Ȱ��ȭ �� ������Ʈ�� ���� ��� ������Ʈ ����
                GameObject infoTmp = Instantiate(itemInfoUI, whiteBoard);
                infoTmp.transform.localPosition = Vector3.zero;
                infoTmp.transform.name = other.transform.name;
                infoTmp.GetComponent<Image>().sprite = other.gameObject.GetComponent<Item>().ItemInfo; // ������ ��ü�� ����ִ� �̹��� ����
                activeInfo.Add(infoTmp);
            } else { // ������ ���� �� ��Ȱ��ȭ �� ������Ʈ�� �ִ� ��� ��Ȱ��
                GameObject infoTmp = unactiveInfo[0];
                infoTmp.gameObject.SetActive(true);
                infoTmp.transform.localPosition = Vector3.zero;
                infoTmp.transform.name = other.transform.name;
                infoTmp.GetComponent<Image>().sprite = other.gameObject.GetComponent<Item>().ItemInfo; // ������ ��ü�� ����ִ� �̹��� ����
                unactiveInfo.Remove(infoTmp);
                activeInfo.Add(infoTmp);
            }
        }
    }

    // Ž�� ���� ������ �������� ����� ���� ������Ʈ ��Ȱ��ȭ
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Item")) {
            // Ȱ��ȭ�� ������Ʈ ����Ʈ ��ȸ�ϸ� ��� ������Ʈ Ž��
            for (int i = 0; i < activeInfo.Count; i++) {
                // ������ �̸����� Ž�� �� ������Ʈ ��Ȱ��ȭ
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
