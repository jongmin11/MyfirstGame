using UnityEngine;

[System.Serializable]
public struct DialogueLine
{
    public Sprite portrait;      // (���߿� ĳ���� �� �ֱ��, �� �� �Ÿ� �� �ٰ� �Ʒ� portrait ���� �ڵ�� ����)
    [TextArea(2, 5)]
    public string text;          // ���
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "Game/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] lines; // ������� ������ ��� ����Ʈ
}
