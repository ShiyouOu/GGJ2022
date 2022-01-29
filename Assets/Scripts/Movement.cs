using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;

    private float _horizontal;
    private float _vertical;
    private float _moveLimiter = 0.7f;

    [SerializeField] private float moveSpeed = 20.0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal, _vertical).normalized * moveSpeed;
    }
}
