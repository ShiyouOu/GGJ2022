using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private int baseDamage;
    [SerializeField] private int health;

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private Stats _stats;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _stats = new Stats(health);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = new Vector2((transform.position.x - target.transform.position.x), (transform.position.y - target.transform.position.y)).normalized * speed;
        _rb.velocity = -velocity;
        CheckHit();
    }

    public void TakeDamage(int dmg)
    {
        dmg = Mathf.Clamp(dmg, 0, int.MaxValue);

        // So hp doesn't go under 0
        if (dmg >= _stats.currentHP)
        {
            _stats.currentHP = 0;
            print("enemy is dead");
        }
        else
        {
            _stats.currentHP -= dmg;
        }
    }

    private void CheckHit()
    {
        Collider2D[] results = new Collider2D[5];
        _collider.OverlapCollider(new ContactFilter2D(), results);

        foreach(Collider2D collider in results)
        {
            if (!collider) { break; }
            Player plr = collider.gameObject.GetComponent<Player>();
            if (plr)
            {
                plr.TakeDamage(baseDamage);
            }
        }
    }
}
