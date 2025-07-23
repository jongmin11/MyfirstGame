using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject eKeyUI; // 여기에 "Canvas" 넣기

    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E 키 입력: 상호작용 실행");
            // 대화 창 열기 등
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eKeyUI.SetActive(true);  // <-- Canvas 단위로 켜기
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eKeyUI.SetActive(false);
            isPlayerNearby = false;
        }
    }
}
