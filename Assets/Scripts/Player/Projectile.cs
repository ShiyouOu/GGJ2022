using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private int maxEnemiesHit = 2;
    private int enemiesHit = 0;
    private List<GameObject> hitEnemies;

    private Collider2D _collider;
    

    // Start is called before the first frame update
    void Start()
    {
        hitEnemies = new List<GameObject>();
        _collider = GetComponent<Collider2D>();
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
                    enemiesHit++;
                    if (enemiesHit >= maxEnemiesHit)
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
