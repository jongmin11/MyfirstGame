using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject eKeyUI; // ���⿡ "Canvas" �ֱ�

    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E Ű �Է�: ��ȣ�ۿ� ����");
            // ��ȭ â ���� ��
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eKeyUI.SetActive(true);  // <-- Canvas ������ �ѱ�
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
