using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    bool isdead = false;
    bool isflapping = false;
    bool godMode = false;
    Rigidbody2D rigidbody2D;    
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
            if(Input.GetKeyDown(KeyCode.Space) && !isflapping)
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
            velocity.y = jumpForce;
            isflapping = false;
        }
       
    }
}
