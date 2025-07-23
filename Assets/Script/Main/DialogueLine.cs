using UnityEngine;

[System.Serializable]
public class DialogueLine
{

    public string speakerName;          // 말하는 사람
    [TextArea(2, 5)]
    public string text;                 // 대사 내용
    public Sprite expression;           // 해당 줄의 표정 (이미지)

    public bool triggerSceneChange = false;
    public string nextSceneName = "";
}