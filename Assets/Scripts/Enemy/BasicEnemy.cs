using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float speed;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = new Vector2((transform.position.x - target.transform.position.x), (transform.position.y - target.transform.position.y)).normalized * speed;
        _rb.velocity = -velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
    }
}
