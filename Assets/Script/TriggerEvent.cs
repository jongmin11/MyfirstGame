using UnityEngine;
using UnityEngine.UI;

public class TriggerEvent : MonoBehaviour
{
    [Header("UI")]
    public GameObject talkButton;          // Canvas 위의 TalkButton
    public DialogueTrigger dialogueTrigger;// 위에서 붙인 컴포넌트

    [Header("Settings")]
    public float verticalOffset = 2f;      // NPC 머리 위 버튼 높이

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
