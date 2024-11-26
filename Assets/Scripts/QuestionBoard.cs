using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoard : MonoBehaviour {
    public Question[] questions; // ���� ���
    public GameObject warning; // ������ �� ����� �������� ���� ������ ���� �� ��µǴ� ��� ������Ʈ 

    /// <summary>
    /// ������ ���� ���� Ȯ��
    /// </summary>
    /// <returns>��� ������ ���� ��� true, Ʋ�� ������ �ִ� ��� false ��ȯ</returns>
    bool ResultCheck() {
        for (int i = 0; i < questions.Length; i++) {
            if (!questions[i].IsRight) {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// ��� ������ ����� ���õƴ��� üũ
    /// </summary>
    /// <returns>��� ���� ������ true, �������� ���� ������ ���� ��� false ��ȯ</returns>
    bool IsSelectedDone() {
        for (int i = 0; i < questions.Length; i++) {
            if (questions[i].s_Answer == -1) {
                return false;
            }
        }

        return true;
    }

    // ��� ����� ���
    // Invoke�� ���� �ð� ���Ŀ� ����
    void HideWarning() {
        warning.SetActive(false);
    }

    // ���� ��� ���� �� �׿� �´� ���� Scene �ε�
    public void OnClickCommitBtn() {
        string name = ""; // ȣ���� scene �̸�

        if (IsSelectedDone()) { // ��� ����� ���õƴ��� üũ
            if (ResultCheck()) { // ��� ���� ������ ���
                name = "TrueEnding";
            } else {
                if (questions[questions.Length - 1].IsRight) { // ������ ������ ���
                    name = "NormalEnding";
                } else { // ���ε� �� ���� ���
                    name = "BadEnding";
                }
            }
        } else { // ����� �������� ���� ������ ���� ��� ��� ���
            CancelInvoke(); // ������ ȣ���� Invoke�� ���� ��츦 ����� Invoke ��Ҹ� �켱������ ����
            warning.SetActive(true); // ��� ���
            Invoke("HideWarning", 1.5f); // 1.5�� �� ����
        }

        GameManager.Instance.ResultSceneLoad(name); // Scene �ε� �Լ� ȣ��
    }
}
