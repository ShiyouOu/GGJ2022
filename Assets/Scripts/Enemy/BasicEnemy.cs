using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private int baseDamage;
    [SerializeField] private int health;
    [SerializeField] private float aggroDelay = 1f;
    [SerializeField] private float timeDelayed = 0f;

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private Stats _stats;
    private Vector3 _spawnLocation;
    private float locationOffset = 0.02f;

    private void OnEnable()
    {
        timeDelayed = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _stats = new Stats(health);
        _spawnLocation = transform.position;
    }

    private void MoveToLocation(Vector3 pos)
    {  
        if((transform.position - pos).magnitude >= locationOffset)
        {
            Vector2 velocity = new Vector2((transform.position.x - pos.x), (transform.position.y - pos.y)).normalized * speed;
            _rb.velocity = -velocity;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeDelayed += Time.deltaTime;
        if (timeDelayed < aggroDelay) { return; }

        if (target)
        {
            MoveToLocation(target.transform.position);
            CheckHit();
        }
        else
        {
            MoveToLocation(_spawnLocation);
        }
    }

    public void TakeDamage(int dmg)
    {
        dmg = Mathf.Clamp(dmg, 0, int.MaxValue);

        // So hp doesn't go under 0
        if (dmg >= _stats.currentHP)
        {
            _stats.currentHP = 0;
            Destroy(gameObject);
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
