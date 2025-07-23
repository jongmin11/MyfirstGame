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
    private bool inputLocked = false;
    public static DialogueManager Instance;
    private DialogueLine currentDialogueLine;
    void Awake()
    {
        Instance = this;
    }


    void Update()
    {
        if (!isTalking || inputLocked) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            AdvanceDialogue();
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
        inputLocked = true;
        Invoke(nameof(UnlockInput), 0.15f); //  약간의 시간차 후 입력 허용
    }

    private void UnlockInput()
    {
        inputLocked = false;
    }

    private void AdvanceDialogue()
    {
        currentLine++;
        if (currentLine >= lines.Length)
        {
            ForceEndDialogue();
        }
        else
        {
            DisplayLine(lines[currentLine]);
        }
    }

    private void DisplayLine(DialogueLine line)
    {
        currentDialogueLine = line; 
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

        if (currentDialogueLine == null)
        {
            Debug.LogError("❌ currentDialogueLine is NULL!");
            return;
        }

        if (!currentDialogueLine.triggerSceneChange)
        {
            Debug.Log("ℹ️ 씬 전환 트리거 아님");
            return;
        }

        if (SceneFader.Instance == null)
        {
            Debug.LogError("❌ SceneFader.Instance is NULL! 씬에 존재하지 않음");
            return;
        }

        Debug.Log("✅ 씬 전환 시도: " + currentDialogueLine.nextSceneName);
        SceneFader.Instance.FadeAndLoadScene(currentDialogueLine.nextSceneName);
    }

    public bool IsTalking() => isTalking;


}