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
    [Header("����� ����")]
    public AudioMixer audioMixer;
    public AudioSource bgSound;

    // ȿ���� Ǯ��
    private Transform usingSFX; // ������� ȿ����
    private Transform usedSFX; // ��Ȱ�� ȿ����

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded; // �� ��ȯ �̺�Ʈ �Ҵ�
        usingSFX = transform.GetChild(0);
        usedSFX = transform.GetChild(1);
        BgSoundPlay("TitleMapBGM");
    }

    public void SFXPlay(string sfxName, AudioClip clip) {
        if (usedSFX.childCount == 0) { // ��Ȱ���� ������Ʈ�� ���� ���
            GameObject clipObj = new GameObject(sfxName + "Sound");
            clipObj.transform.parent = usingSFX;

            // Ŭ�� �ʱ�ȭ �� ���
            AudioSource audiosource = clipObj.AddComponent<AudioSource>();
            audiosource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
            audiosource.clip = clip;
            audiosource.Play();

            // Ŭ�� Ǯ���� ������Ʈ �ʱ�ȭ
            SFXPlayer sfxtmp = clipObj.AddComponent<SFXPlayer>();
            sfxtmp.SetPlayer(usedSFX);
        } else { // ��Ȱ���� ������Ʈ�� ���� ���
            GameObject clipObj = usedSFX.GetChild(0).gameObject;
            clipObj.SetActive(true);
            clipObj.transform.name = sfxName + "Sound";
            clipObj.transform.parent = usingSFX;

            clipObj.GetComponent<AudioSource>().clip = clip;
            clipObj.GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// ��� ���� ���
    /// </summary>
    /// <param name="bgmName">����� ����� Ŭ�� ���ϸ�.Ȯ����</param>
    public void BgSoundPlay(string bgmName) {
        // �̹� ������� Ŭ���� �ٽ� ����Ǵ� ������ �����ϱ� ����
        if (bgSound.clip != null && bgSound.clip.name == bgmName) {
            return;
        }

        bgSound.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        bgSound.clip = Resources.Load<AudioClip>("AudioSources/" + bgmName);
        bgSound.loop = true;
        bgSound.Play();
    }

    // ���� ���� ����
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

    // ��ȯ�� Scene�� ���� Scene�� ��� ���� ����� ���
    // �޸� ����ȭ�� ���� �� ��ȯ�ø��� ���ҽ� ����
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "TrueEnding" || scene.name == "NormalEnding" || scene.name == "BadEnding") {
            BgSoundPlay("ResultSceneBGM");
        }
        // �� �ε尡 �Ϸ�� �� ���ʿ��� ���ҽ��� ����
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }

    /// <summary>
    /// ����� ���� �ٸ� ���� Scene�� �ε�
    /// </summary>
    /// <param name="sceneName">����� �´� Scene �̸�</param>
    public void ResultSceneLoad(string sceneName) {
        if (sceneName == "")
            return;
        else {
            StartCoroutine(LoadingAsync(sceneName));
        }
    }

    // �޸� Ȯ���� ���� �� �ε� ����
    IEnumerator LoadingAsync(string name) {
        SceneManager.LoadScene("LoadingScene");
        yield return new WaitForSeconds(0.5f); // ª�� �������� �޸� Ȯ�� �ð��� ��

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = true; //�ε��� �Ϸ�Ǵ´�� ���� Ȱ��ȭ�Ұ�����
        yield return null;
    }

    public void ExitGame() {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        Application.Quit();
    }
}
