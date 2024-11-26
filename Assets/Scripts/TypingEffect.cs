using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 엔딩 Scene에서 엔딩 스토리 텍스트에 타이핑 효과를 주는 객체
/// </summary>
public class TypingEffect : MonoBehaviour
{
    public Text txt; // 엔딩 스토리가 타이핑 될 Text 오브젝트
    public Text exitTxt; // 게임 종료 버튼 대용으로 사용될 Text 오브젝트
    [Multiline] public string[] comment; // 엔딩 스토리 배열
    public AudioClip[] typingSounds; // 타이핑 효과음으로 사용될 클립 배열

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Typing(0));
    }

    /// <summary>
    /// 엔딩 텍스트를 타이핑 효과 적용해서 출력
    /// 타이핑 효과음도 함께 출력
    /// </summary>
    /// <param name="idx">현재 출력할 텍스트 인덱스</param>
    /// <returns></returns>
    IEnumerator Typing(int idx) {
        // 반복문을 활용해 텍스트를 한 글자씩 출력
        for (int i = 0; i <= comment[idx].Length; i++) {
            int num = Random.Range(0, typingSounds.Length); // 재생할 타이핑 효과음 랜덤 인덱스
            GameManager.Instance.SFXPlay("typing", typingSounds[num]); // 효과음 재생
            txt.text = comment[idx].Substring(0, i); // 한 글자씩 추가하여 출력

            yield return new WaitForSeconds(0.08f);
        }

        // 현재 텍스트의 출력이 끝나면 인덱스 증가
        // 출력할 텍스트가 남아있다면 1.5초 후 다음 텍스트 출력을 위한 코루틴 실행
        // 더이상 출력할 텍스트가 없을 경우 게임 종료 버튼을 대신할 게임 종료 텍스트 출력
        idx++; 
        if(idx < comment.Length) { 
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(Typing(idx));
        } else {
            string ending = "게임 종료";
            for (int i = 0; i <= ending.Length; i++) {
                int num = Random.Range(0, typingSounds.Length); // 재생할 타이핑 효과음 랜덤 인덱스
                GameManager.Instance.SFXPlay("typing", typingSounds[num]); // 효과음 재생
                exitTxt.text = ending.Substring(0, i); // 한 글자씩 추가하여 출력

                yield return new WaitForSeconds(0.08f);
            }
            // 텍스트에 버튼 기능 활성화 후 이벤트 등록
            exitTxt.GetComponent<Button>().enabled = true;
            exitTxt.GetComponent<Button>().onClick.AddListener(GameManager.Instance.ExitGame);
        }
    }
}
