using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    bool isdead = false;
    bool isflapping = false;
    public bool godMode = false;
     new Rigidbody2D rigidbody2D;    
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isdead)
        {
            return;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                isflapping = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isdead)
        {
            return;
        }
        
        Vector3 velocity = rigidbody2D.velocity;
        velocity.x = moveSpeed;

        if (isflapping)
        {
            velocity.y += jumpForce;
            isflapping = false;
        }

        rigidbody2D.velocity = velocity;
        float angle = Mathf.Clamp((rigidbody2D.velocity.y * 10), -90f, 90f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode)
        {
            return;
        }

        if (isdead)
        {
            return;
        }

        isdead = true;
        GameManager.Instance.GameOver();
    }

    public void OnScore()
    {
        // 현재는 아무 것도 안 해도 괜찮음
    }
}
