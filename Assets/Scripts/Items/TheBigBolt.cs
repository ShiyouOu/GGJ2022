using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBigBolt : MonoBehaviour
{
    [SerializeField] private int _damageMod;
    [SerializeField] private int _karmaMod;

    private Collider2D _collider;

    public void HitDetection()
    {
        Collider2D[] results = new Collider2D[5];
        _collider.OverlapCollider(new ContactFilter2D(), results);

        foreach (Collider2D collider in results)
        {
            if (!collider) { break; }
            Player plr = collider.gameObject.GetComponent<Player>();
            if (plr)
            {
                plr.gameObject.GetComponent<Player>().AddKarma(_karmaMod);
                plr.gameObject.GetComponent<Player>().AddDamage(_damageMod);

                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HitDetection();
    }
}
