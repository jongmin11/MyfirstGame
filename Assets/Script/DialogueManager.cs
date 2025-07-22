using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public GameObject dialogueUI;    // Panel ������Ʈ
    public Text nameText;            // NPC �̸� ǥ�ÿ�
    public TMP_Text dialogueText;        // ��� �ؽ�Ʈ ǥ�ÿ�

    private Queue<DialogueLine> linesQueue;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        linesQueue = new Queue<DialogueLine>();
        dialogueUI.SetActive(false);
    }

    public void StartDialogue(DialogueData data, string npcName)
    {
        dialogueUI.SetActive(true);
        nameText.text = npcName;
        linesQueue.Clear();
        foreach (var line in data.lines)
            linesQueue.Enqueue(line);
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (linesQueue.Count == 0)
        {
            dialogueUI.SetActive(false);
            return;
        }
        var line = linesQueue.Dequeue();
        dialogueText.text = line.text;
    }
}
