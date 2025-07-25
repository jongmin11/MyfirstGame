using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody2D;

    [SerializeField] private SpriteRenderer chracterRenderer;
    [SerializeField] private Transform wenponPivot;
    
    protected Vector2 movementDirection = Vector2.zero; 

    public Vector2 MovementDirection { get { return movementDirection; } }
    
    protected Vector2 lookDirection = Vector2.zero;
    
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0f;

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movement(Vector2 direction)
    {
        direction = direction * 5;
        if (knockbackDuration > 0.0f)
        {
            direction += Vector2.one * 0.2f;
            direction += knockback;
        }

        _rigidbody2D.velocity = direction;
    }


    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) < 90f;

        chracterRenderer.flipX = isLeft;

        if(wenponPivot != null)
        {
            wenponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }

    public void Applyknockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = (other.position - transform.position). normalized * power;
    }
}
