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
        float adjustedMoveSpeed;
        if (Player.instance.karma > 0)
        {
            adjustedMoveSpeed = (float)(moveSpeed + (Player.instance.karma * 0.1));
        }
        else
        {
            adjustedMoveSpeed = moveSpeed;
        }
        Vector2 moveDir = _plrInput.GetMoveDir();
        _rb.velocity = new Vector2(moveDir.x, moveDir.y).normalized * adjustedMoveSpeed;
    }
}
