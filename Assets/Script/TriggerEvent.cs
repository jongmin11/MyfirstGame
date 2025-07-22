using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{

    public string playerTag = "Player";

    public UnityEvent onEnter;
    public UnityEvent onExit;

    public void Awake()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            onEnter?.Invoke();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            onExit?.Invoke();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
