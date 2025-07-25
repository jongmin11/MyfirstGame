using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera camera;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }
}
