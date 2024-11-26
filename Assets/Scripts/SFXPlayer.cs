using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 효과음 오브젝트 풀링을 위해 효과음이 끝나면 비활성화 해주는 객체
/// </summary>
public class SFXPlayer : MonoBehaviour
{
    private Transform usedSFX;

    // Update is called once per frame
    void Update()
    {
        // 효과음이 현재 재생중이지 않을 경우 오브젝트 비활성화
        if (!GetComponent<AudioSource>().isPlaying) {
            transform.parent = usedSFX;
            gameObject.SetActive(false);
        }
    }

    public void SetPlayer(Transform usedSFX) {
        this.usedSFX = usedSFX;
    }
}
