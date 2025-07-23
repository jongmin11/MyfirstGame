using System.Linq;
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
    private bool inputBlocked = false;
    public static DialogueManager Instance;

    void Awake()
    {
        Instance = this;
    }

    private bool inputReady = false;

    void Update()
    {
        if (!isTalking) return;

        // 키 떼기 감지
        if (Input.GetKeyUp(KeyCode.E))
        {
            inputReady = true;
        }

        // 키 누르기 감지 (한 번 떴다가 다시 눌렸을 때만)
        if (Input.GetKeyDown(KeyCode.E) && inputReady)
        {
            AdvanceDialogue();
            inputReady = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EndDialogue();
        }
    }

    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.lines == null || data.lines.Length == 0)
        {
            Debug.LogWarning("대사 데이터 없음");
            return;
        }

        lines = data.lines.ToArray();
        currentLine = 0;
        isTalking = true;
        talkPanel.SetActive(true);
        DisplayLine(lines[currentLine]);
        Invoke(nameof(UnlockInput), 0.1f);
    }

    private void UnlockInput()
    {
        inputBlocked = false;
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