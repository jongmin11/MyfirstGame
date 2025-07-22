using UnityEngine;
using UnityEngine.UI;

public class TriggerEvent : MonoBehaviour
{
    [Header("UI")]
    public GameObject talkButton;          // Canvas ���� TalkButton
    public DialogueTrigger dialogueTrigger;// ������ ���� ������Ʈ

    [Header("Settings")]
    public float verticalOffset = 2f;      // NPC �Ӹ� �� ��ư ����

    private Camera mainCam;
    private RectTransform buttonRect;

    void Awake()
    {
        mainCam = Camera.main;
        buttonRect = talkButton.GetComponent<RectTransform>();
        talkButton.SetActive(false);
        talkButton.GetComponent<Button>()
                  .onClick.AddListener(dialogueTrigger.TriggerDialogue);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) talkButton.SetActive(true);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) talkButton.SetActive(false);
    }

    void Update()
    {
        if (!talkButton.activeSelf) return;
        Vector3 worldPos = transform.position + Vector3.up * verticalOffset;
        buttonRect.position = mainCam.WorldToScreenPoint(worldPos);
    }
}
