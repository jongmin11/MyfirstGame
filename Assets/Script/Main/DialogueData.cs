using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Table-Based Dialogue")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] lines;   // 한 줄마다 이름 + 대사 + 표정 다 포함
}