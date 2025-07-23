using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text nameText;
    public TMP_Text dialogueText;
    public Image portraitImage;

    private DialogueLine[] lines;
    private int currentLine;
    private bool isTalking = false;

    public static DialogueManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.E))
            AdvanceDialogue();
    }

    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.lines.Length == 0) return;

        lines = data.lines;
        currentLine = 0;
        isTalking = true;
        talkPanel.SetActive(true);
        DisplayLine(lines[currentLine]);
    }

    void AdvanceDialogue()
    {
        currentLine++;
        if (currentLine >= lines.Length)
        {
            EndDialogue();
        }
        else
        {
            DisplayLine(lines[currentLine]);
        }
    }

    void DisplayLine(DialogueLine line)
    {
        nameText.text = line.speakerName;
        dialogueText.text = line.text;
        portraitImage.sprite = line.expression;
    }

    void EndDialogue()
    {
        isTalking = false;
        talkPanel.SetActive(false);
    }

    public bool IsTalking() => isTalking;
}