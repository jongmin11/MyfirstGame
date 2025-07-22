using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("â�� ������")]
    public DialogueData dialogueData; // ������ ���� ScriptableObject

    [Header("NPC �̸�")]
    public string npcName;            // Inspector���� NPC �̸� �Է�

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogueData, npcName);
    }
}
