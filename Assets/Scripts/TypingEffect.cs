using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� Scene���� ���� ���丮 �ؽ�Ʈ�� Ÿ���� ȿ���� �ִ� ��ü
/// </summary>
public class TypingEffect : MonoBehaviour
{
    public Text txt; // ���� ���丮�� Ÿ���� �� Text ������Ʈ
    public Text exitTxt; // ���� ���� ��ư ������� ���� Text ������Ʈ
    [Multiline] public string[] comment; // ���� ���丮 �迭
    public AudioClip[] typingSounds; // Ÿ���� ȿ�������� ���� Ŭ�� �迭

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Typing(0));
    }

    /// <summary>
    /// ���� �ؽ�Ʈ�� Ÿ���� ȿ�� �����ؼ� ���
    /// Ÿ���� ȿ������ �Բ� ���
    /// </summary>
    /// <param name="idx">���� ����� �ؽ�Ʈ �ε���</param>
    /// <returns></returns>
    IEnumerator Typing(int idx) {
        // �ݺ����� Ȱ���� �ؽ�Ʈ�� �� ���ھ� ���
        for (int i = 0; i <= comment[idx].Length; i++) {
            int num = Random.Range(0, typingSounds.Length); // ����� Ÿ���� ȿ���� ���� �ε���
            GameManager.Instance.SFXPlay("typing", typingSounds[num]); // ȿ���� ���
            txt.text = comment[idx].Substring(0, i); // �� ���ھ� �߰��Ͽ� ���

            yield return new WaitForSeconds(0.08f);
        }

        // ���� �ؽ�Ʈ�� ����� ������ �ε��� ����
        // ����� �ؽ�Ʈ�� �����ִٸ� 1.5�� �� ���� �ؽ�Ʈ ����� ���� �ڷ�ƾ ����
        // ���̻� ����� �ؽ�Ʈ�� ���� ��� ���� ���� ��ư�� ����� ���� ���� �ؽ�Ʈ ���
        idx++; 
        if(idx < comment.Length) { 
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(Typing(idx));
        } else {
            string ending = "���� ����";
            for (int i = 0; i <= ending.Length; i++) {
                int num = Random.Range(0, typingSounds.Length); // ����� Ÿ���� ȿ���� ���� �ε���
                GameManager.Instance.SFXPlay("typing", typingSounds[num]); // ȿ���� ���
                exitTxt.text = ending.Substring(0, i); // �� ���ھ� �߰��Ͽ� ���

                yield return new WaitForSeconds(0.08f);
            }
            // �ؽ�Ʈ�� ��ư ��� Ȱ��ȭ �� �̺�Ʈ ���
            exitTxt.GetComponent<Button>().enabled = true;
            exitTxt.GetComponent<Button>().onClick.AddListener(GameManager.Instance.ExitGame);
        }
    }
}
