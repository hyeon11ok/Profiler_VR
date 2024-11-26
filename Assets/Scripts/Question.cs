using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ����� ������ ��, ���� ���� ���� �����ϴ� ��ü
/// </summary>
public class Question : MonoBehaviour
{
    [SerializeField] private int answer; // ���� ��ȣ
    private int selectedAnswer = -1;
    [SerializeField] private Text[] answerList; // ���� ���
    private Text selectAnswer; // ���� ���õ� ����
    private bool isRight = false; // ���� ����

    public bool IsRight { get { return isRight; } }
    public int s_Answer { get { return selectedAnswer; } }


    /// <summary>
    /// ���� ��� ����
    /// </summary>
    /// <param name="num">������ ��� ��ȣ</param>
    public void SelectedAnswer(int num) {
        selectedAnswer = num;

        // ���� ���� ����� ���� ��� ���� �ʱ�ȭ
        if (selectAnswer != null) {
            selectAnswer.color = Color.white;
        }

        answerList[num - 1].color = Color.red;
        selectAnswer = answerList[num - 1];
        if(num == answer)
            isRight = true;
        else
            isRight = false;
    }
}
