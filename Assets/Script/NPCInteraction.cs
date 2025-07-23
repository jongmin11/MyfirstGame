using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject eKeyUI;
    public DialogueData dialogueData;

    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {   
            DialogueManager.Instance.StartDialogue(dialogueData);
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

            // 대화 중이 아닐 때만 끊음
            if (!DialogueManager.Instance.IsTalking())
            {
                isPlayerNearby = false;
            }
        }
    }
}
