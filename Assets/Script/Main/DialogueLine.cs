using UnityEngine;

[System.Serializable]
public class DialogueLine
{

    public string speakerName;          // ���ϴ� ���
    [TextArea(2, 5)]
    public string text;                 // ��� ����
    public Sprite expression;           // �ش� ���� ǥ�� (�̹���)

    public bool triggerSceneChange = false;
    public string nextSceneName = "";
}