using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // AudioSetting
    [Header("오디오 세팅")]
    public AudioMixer audioMixer;
    public AudioSource bgSound;

    // 효과음 풀링
    private Transform usingSFX; // 사용중인 효과음
    private Transform usedSFX; // 비활성 효과음

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 전환 이벤트 할당
        usingSFX = transform.GetChild(0);
        usedSFX = transform.GetChild(1);
        BgSoundPlay("TitleMapBGM");
    }

    public void SFXPlay(string sfxName, AudioClip clip) {
        if (usedSFX.childCount == 0) { // 재활용할 오브젝트가 없을 경우
            GameObject clipObj = new GameObject(sfxName + "Sound");
            clipObj.transform.parent = usingSFX;

            // 클립 초기화 및 재생
            AudioSource audiosource = clipObj.AddComponent<AudioSource>();
            audiosource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
            audiosource.clip = clip;
            audiosource.Play();

            // 클립 풀링용 컴포넌트 초기화
            SFXPlayer sfxtmp = clipObj.AddComponent<SFXPlayer>();
            sfxtmp.SetPlayer(usedSFX);
        } else { // 재활용할 오브젝트가 있을 경우
            GameObject clipObj = usedSFX.GetChild(0).gameObject;
            clipObj.SetActive(true);
            clipObj.transform.name = sfxName + "Sound";
            clipObj.transform.parent = usingSFX;

            clipObj.GetComponent<AudioSource>().clip = clip;
            clipObj.GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// 배경 음악 재생
    /// </summary>
    /// <param name="bgmName">재생할 배경음 클립 파일명.확장자</param>
    public void BgSoundPlay(string bgmName) {
        // 이미 재생중인 클립이 다시 재생되는 현상을 방지하기 위함
        if (bgSound.clip != null && bgSound.clip.name == bgmName) {
            return;
        }

        bgSound.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        bgSound.clip = Resources.Load<AudioClip>("AudioSources/" + bgmName);
        bgSound.loop = true;
        bgSound.Play();
    }

    // 음향 볼륨 조절
    #region Sound Setting
    public void MasterVolume(float val) {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(val) * 20);
    }

    public void BGMVolume(float val) {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(val) * 20);
    }

    public void SFXVolume(float val) {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(val) * 20);
    }
    #endregion

    // 전환된 Scene이 엔딩 Scene일 경우 엔딩 배경음 재생
    // 메모리 최적화를 위해 씬 전환시마다 리소스 정리
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "TrueEnding" || scene.name == "NormalEnding" || scene.name == "BadEnding") {
            BgSoundPlay("ResultSceneBGM");
        }
        // 씬 로드가 완료된 후 불필요한 리소스를 정리
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }

    /// <summary>
    /// 결과에 따라 다른 엔딩 Scene을 로드
    /// </summary>
    /// <param name="sceneName">결과에 맞는 Scene 이름</param>
    public void ResultSceneLoad(string sceneName) {
        if (sceneName == "")
            return;
        else {
            StartCoroutine(LoadingAsync(sceneName));
        }
    }

    // 메모리 확보를 위한 씬 로드 지연
    IEnumerator LoadingAsync(string name) {
        SceneManager.LoadScene("LoadingScene");
        yield return new WaitForSeconds(0.5f); // 짧은 지연으로 메모리 확보 시간을 줌

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = true; //로딩이 완료되는대로 씬을 활성화할것인지
        yield return null;
    }

    public void ExitGame() {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        Application.Quit();
    }
}
