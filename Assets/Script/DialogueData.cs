using UnityEngine;

[System.Serializable]
public struct DialogueLine
{
    public Sprite portrait;      // (나중에 캐릭터 얼굴 넣기용, 안 쓸 거면 이 줄과 아래 portrait 관련 코드는 무시)
    [TextArea(2, 5)]
    public string text;          // 대사
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "Game/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] lines; // 순서대로 보여줄 대사 리스트
}
