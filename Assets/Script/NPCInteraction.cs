using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject eKeyUI;
    public DialogueData dialogueData;

    private bool isPlayerNearby = false;

    private bool hasSpoken = false;

    void Update()
    {
        // 대화 시작
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!DialogueManager.Instance.IsTalking() && !hasSpoken)
            {
                DialogueManager.Instance.StartDialogue(dialogueData);
                hasSpoken = true;
            }
        }

        // 대화가 끝나면 다시 대화 가능하도록 초기화
        if (!DialogueManager.Instance.IsTalking() && hasSpoken)
        {
            hasSpoken = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eKeyUI.SetActive(true);
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eKeyUI.SetActive(false);
            isPlayerNearby = false;

            if (DialogueManager.Instance.IsTalking())
            {
                DialogueManager.Instance.ForceEndDialogue();
            }
        }
    }


}