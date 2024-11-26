using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoard : MonoBehaviour {
    public Question[] questions; // 문제 목록
    public GameObject warning; // 문제들 중 답안을 선택하지 않은 문제가 있을 시 출력되는 경고문 오브젝트 

    /// <summary>
    /// 문제의 정답 여부 확인
    /// </summary>
    /// <returns>모든 문제를 맞출 경우 true, 틀린 문제가 있는 경우 false 반환</returns>
    bool ResultCheck() {
        for (int i = 0; i < questions.Length; i++) {
            if (!questions[i].IsRight) {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 모든 문제의 답안이 선택됐는지 체크
    /// </summary>
    /// <returns>모두 선택 됐으면 true, 선택하지 않은 문제가 있을 경우 false 반환</returns>
    bool IsSelectedDone() {
        for (int i = 0; i < questions.Length; i++) {
            if (questions[i].s_Answer == -1) {
                return false;
            }
        }

        return true;
    }

    // 경고문 숨기는 기능
    // Invoke로 일정 시간 이후에 숨김
    void HideWarning() {
        warning.SetActive(false);
    }

    // 게임 결과 산출 후 그에 맞는 엔딩 Scene 로드
    public void OnClickCommitBtn() {
        string name = ""; // 호출할 scene 이름

        if (IsSelectedDone()) { // 모든 답안이 선택됐는지 체크
            if (ResultCheck()) { // 모든 답을 맞췄을 경우
                name = "TrueEnding";
            } else {
                if (questions[questions.Length - 1].IsRight) { // 범인을 맞췄을 경우
                    name = "NormalEnding";
                } else { // 범인도 못 맞춘 경우
                    name = "BadEnding";
                }
            }
        } else { // 답안을 선택하지 않은 문제가 있을 경우 경고문 출력
            CancelInvoke(); // 이전에 호출한 Invoke가 있을 경우를 대비해 Invoke 취소를 우선적으로 실행
            warning.SetActive(true); // 경고문 출력
            Invoke("HideWarning", 1.5f); // 1.5초 후 숨김
        }

        GameManager.Instance.ResultSceneLoad(name); // Scene 로드 함수 호출
    }
}
