using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float knockbackMod = 1;
    private int enemiesHit = 0;
    private List<GameObject> hitEnemies;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private int _maxEnemiesHit;
    

    // Start is called before the first frame update
    void Start()
    {
        hitEnemies = new List<GameObject>();
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        if(Player.instance.karma >= 10)
        {
            _maxEnemiesHit = (int)Mathf.Round(Player.instance.karma / 10);
        }
        else
        {
            _maxEnemiesHit = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckHit();
    }


    private void CheckHit()
    {
        Collider2D[] results = new Collider2D[5];
        _collider.OverlapCollider(new ContactFilter2D(), results);
        bool hitObs = false; 

        foreach (Collider2D collider in results)
        {
            if (!collider) { break; }
            IDamageable dmgAble = collider.gameObject.GetComponent<IDamageable>();
            if (dmgAble != null)
            {
                if (!hitEnemies.Contains(collider.gameObject))
                {
                    hitEnemies.Add(collider.gameObject);
                    dmgAble.TakeDamage(damage);
                    if (collider == null)
                    {
                        break;
                    }
                    if (collider.gameObject.GetComponent<BasicEnemy>())
                    {
                        collider.gameObject.GetComponent<Rigidbody2D>().AddForce(_rigidbody.velocity * knockbackMod);
                    }
                    enemiesHit++;
                    if (enemiesHit >= _maxEnemiesHit)
                    {
                        Destroy(gameObject);
                        break;
                    }
                }
            }
            else if (collider.gameObject.layer == 6 || collider.gameObject.layer == 7)
            {
                hitObs = true;
            }
        }
        if (hitObs)
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }
}
