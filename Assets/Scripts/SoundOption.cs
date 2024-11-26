using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// 사운드 설정 UI 초기화 객체
/// 다른 Scene으로 넘어가는 상황을 염두해 만든 객체
/// 최초 실행시 각 UI에 이벤트 할당과 현재 사운드 설정값 동기화 기능
/// </summary>
public class SoundOption : MonoBehaviour
{
    // 오디오 믹서와 각 사운드를 설정할 슬라이더 UI변수
    public AudioMixer audioMixer;
    public Slider m_slider; // 전체 음량 조절 슬라이더
    public Slider b_slider; // 배경음 음량 조절 슬라이더
    public Slider s_slider; // 효과음 음량 조절 슬라이더

    private void Start() {
        // 각 슬라이더에 이벤트 할당
        m_slider.onValueChanged.AddListener(GameManager.Instance.MasterVolume);
        b_slider.onValueChanged.AddListener(GameManager.Instance.BGMVolume);
        s_slider.onValueChanged.AddListener(GameManager.Instance.SFXVolume);

        // 현재 각 사운드 음량을 받아올 변수
        float m_volume; // 전체 음량
        float b_volume; // 배경음 음량
        float s_volume; // 효과음 음량

        // 현재 각 음량 받아오기
        audioMixer.GetFloat("MasterVolume", out m_volume);
        audioMixer.GetFloat("BGMVolume", out b_volume);
        audioMixer.GetFloat("SFXVolume", out s_volume);

        // 각 슬라이더의 값에 현재 음량 동기화
        m_slider.value = Mathf.Pow(10, m_volume / 20);
        b_slider.value = Mathf.Pow(10, b_volume / 20);
        s_slider.value = Mathf.Pow(10, s_volume / 20);
    }
}
