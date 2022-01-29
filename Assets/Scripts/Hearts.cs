using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    [SerializeField] private int _heal = 1;
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
                HealPlayer(_heal);
            }
        }
    }
    public void HealPlayer(int heal)
    {
        //heal = Mathf.Clamp(_heal, 0, int.MaxValue);

         Player.instance.HealPlayer(heal);
         Destroy(gameObject);
        
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
