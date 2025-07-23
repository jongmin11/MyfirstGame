using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/NPC Dialogue")]
public class DialogueData : ScriptableObject
{
    public string npcName;
    [TextArea(2, 5)]
    public string[] lines;
}