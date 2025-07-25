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

    private Vector2 konckback = Vector2.zero;
    private float knockbackDuretion = 0f;

    protected virtual void Awake()
    {

    }
}
