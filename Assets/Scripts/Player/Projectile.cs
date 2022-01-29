using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private Collider2D _collider;
    

    // Start is called before the first frame update
    void Start()
    {
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

        foreach (Collider2D collider in results)
        {
            if (!collider) { break; }
            IDamageable dmgAble = collider.gameObject.GetComponent<IDamageable>();
            if (dmgAble != null)
            {
                dmgAble.TakeDamage(damage);
            }
        }
    }
}
