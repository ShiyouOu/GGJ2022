using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;

    private PlayerInput _plrInput;

    [SerializeField] private float moveSpeed = 20.0f;

    void Start()
    {
        _plrInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 moveDir = _plrInput.GetMoveDir();
        _rb.velocity = new Vector2(moveDir.x, moveDir.y).normalized * moveSpeed;
    }
}
