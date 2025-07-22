using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("창고 데이터")]
    public DialogueData dialogueData; // 위에서 만든 ScriptableObject

    [Header("NPC 이름")]
    public string npcName;            // Inspector에서 NPC 이름 입력

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogueData, npcName);
    }
}
