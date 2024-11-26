using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// ���� ���� UI �ʱ�ȭ ��ü
/// �ٸ� Scene���� �Ѿ�� ��Ȳ�� ������ ���� ��ü
/// ���� ����� �� UI�� �̺�Ʈ �Ҵ�� ���� ���� ������ ����ȭ ���
/// </summary>
public class SoundOption : MonoBehaviour
{
    // ����� �ͼ��� �� ���带 ������ �����̴� UI����
    public AudioMixer audioMixer;
    public Slider m_slider; // ��ü ���� ���� �����̴�
    public Slider b_slider; // ����� ���� ���� �����̴�
    public Slider s_slider; // ȿ���� ���� ���� �����̴�

    private void Start() {
        // �� �����̴��� �̺�Ʈ �Ҵ�
        m_slider.onValueChanged.AddListener(GameManager.Instance.MasterVolume);
        b_slider.onValueChanged.AddListener(GameManager.Instance.BGMVolume);
        s_slider.onValueChanged.AddListener(GameManager.Instance.SFXVolume);

        // ���� �� ���� ������ �޾ƿ� ����
        float m_volume; // ��ü ����
        float b_volume; // ����� ����
        float s_volume; // ȿ���� ����

        // ���� �� ���� �޾ƿ���
        audioMixer.GetFloat("MasterVolume", out m_volume);
        audioMixer.GetFloat("BGMVolume", out b_volume);
        audioMixer.GetFloat("SFXVolume", out s_volume);

        // �� �����̴��� ���� ���� ���� ����ȭ
        m_slider.value = Mathf.Pow(10, m_volume / 20);
        b_slider.value = Mathf.Pow(10, b_volume / 20);
        s_slider.value = Mathf.Pow(10, s_volume / 20);
    }
}
