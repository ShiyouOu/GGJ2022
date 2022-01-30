using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikey : MonoBehaviour
{
    public AudioClip clip;

    [SerializeField] private int dmg;
    private Collider2D _collider;
    private Rigidbody2D _rigidBody;
    private bool _triggered = false;
    private bool _dmgDealt = false; 
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SmashDir(Vector2.up);
        SmashDir(Vector2.down);
        SmashDir(Vector2.left);
        SmashDir(Vector2.right);
        if (!_dmgDealt)
        {
            CheckHit();
        }
        if (_rigidBody.velocity.magnitude < 5 && _dmgDealt)
        {
            Destroy(gameObject);
        }
    }

    private bool SmashDir(Vector2 dir)
    {
        if (_triggered)
        {
            return false; 
        }
        RaycastHit2D[] hitResults = new RaycastHit2D[5];
        Physics2D.BoxCast(transform.position, new Vector2(_collider.bounds.size.x, _collider.bounds.size.x), 0f, dir, new ContactFilter2D(), hitResults);
        foreach (RaycastHit2D hit in hitResults)
        {
            if (hit == null)
            {
                break;
            }
            if (hit.collider == null)
            {
                continue;
            }
            if (hit.collider.gameObject.tag == "Player")
            {
                _triggered = true;
                _rigidBody.AddForce(dir * 500000);
                return true;
            }
        }
        return false;
    }
    private void CheckHit()
    {
        Collider2D[] results = new Collider2D[5];
        _collider.OverlapCollider(new ContactFilter2D(), results);

        foreach (Collider2D collider in results)
        {
            if (!collider) { break; }
            Player plr = collider.gameObject.GetComponent<Player>();
            if (plr)
            {
                SoundManager.Instance.PlayEffect(clip, transform.position);
                plr.TakeDamage(dmg);
                _dmgDealt = true;
            }
            else if (collider.gameObject.layer == 6 || collider.gameObject.layer == 7 || collider.gameObject.layer == 11)
            {
                SoundManager.Instance.PlayEffect(clip, transform.position);
                Destroy(gameObject);

            }
        }
    }
}
