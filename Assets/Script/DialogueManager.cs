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
    private int currentLine = 0;
    private bool isTalking = false;

    public static DialogueManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!isTalking) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            AdvanceDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ForceEndDialogue();
        }
    }

    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.lines == null || data.lines.Length == 0)
        {
            Debug.LogWarning("대사 데이터 없음");
            return;
        }

        lines = data.lines;
        currentLine = 0;
        isTalking = true;
        talkPanel.SetActive(true);
        DisplayLine(lines[currentLine]);
    }

    private void AdvanceDialogue()
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

    private void DisplayLine(DialogueLine line)
    {
        nameText.text = line.speakerName;
        dialogueText.text = line.text;
        portraitImage.sprite = line.expression;
    }

    public void EndDialogue()
    {
        isTalking = false;
        talkPanel.SetActive(false);
    }

    public void ForceEndDialogue()
    {
        EndDialogue();
    }

    public bool IsTalking() => isTalking;
}