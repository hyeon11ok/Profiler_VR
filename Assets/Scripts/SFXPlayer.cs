using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ȿ���� ������Ʈ Ǯ���� ���� ȿ������ ������ ��Ȱ��ȭ ���ִ� ��ü
/// </summary>
public class SFXPlayer : MonoBehaviour
{
    private Transform usedSFX;

    // Update is called once per frame
    void Update()
    {
        // ȿ������ ���� ��������� ���� ��� ������Ʈ ��Ȱ��ȭ
        if (!GetComponent<AudioSource>().isPlaying) {
            transform.parent = usedSFX;
            gameObject.SetActive(false);
        }
    }

    public void SetPlayer(Transform usedSFX) {
        this.usedSFX = usedSFX;
    }
}
