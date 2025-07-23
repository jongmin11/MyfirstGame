using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TMP_Text dialogueText;
    public Text npcNameText;

    private string[] lines;
    private int currentLine;
    private bool isTalking = false;

    public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.E))
        {
            AdvanceLine();
        }
    }

    public void StartDialogue(DialogueData data)
    {
        Debug.Log("TalkPanel È°¼ºÈ­");
        talkPanel.SetActive(true);
        lines = data.lines;
        npcNameText.text = data.npcName;
        currentLine = 0;
        isTalking = true;
        dialogueText.text = lines[currentLine];
    }

    void AdvanceLine()
    {
        currentLine++;
        if (currentLine >= lines.Length)
        {
            EndDialogue();
        }
        else
        {
            dialogueText.text = lines[currentLine];
        }
    }

    void EndDialogue()
    {
        talkPanel.SetActive(false);
        isTalking = false;
    }

    public bool IsTalking()
    {
        return isTalking;
    }
}
