using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 문제의 정답과 선택한 답, 정답 여부 등을 저장하는 객체
/// </summary>
public class Question : MonoBehaviour
{
    [SerializeField] private int answer; // 정답 번호
    private int selectedAnswer = -1;
    [SerializeField] private Text[] answerList; // 보기 목록
    private Text selectAnswer; // 현재 선택된 보기
    private bool isRight = false; // 정답 여부

    public bool IsRight { get { return isRight; } }
    public int s_Answer { get { return selectedAnswer; } }


    /// <summary>
    /// 선택 답안 변경
    /// </summary>
    /// <param name="num">선택한 답안 번호</param>
    public void SelectedAnswer(int num) {
        selectedAnswer = num;

        // 이전 선택 답안이 있을 경우 색을 초기화
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
