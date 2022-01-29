using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabby : MonoBehaviour
{
    [SerializeField] private float _retractDur = 5f;
    [SerializeField] private float _stabDur = 5f;
    [SerializeField] private int dmg;
    [SerializeField] private Color _retractColor;
    [SerializeField] private Color _extendColor; 
    private bool _extended = false;
    private float _timeSinceLastState = 0f;
    private Collider2D _collider; 
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastState += Time.deltaTime;
        if (_extended)
        {
            if (_timeSinceLastState > _stabDur)
            {
                _extended = false;
                _timeSinceLastState = 0f;
                GetComponent<SpriteRenderer>().color = _retractColor;
            }
        }
        else
        {
            if (_timeSinceLastState > _retractDur)
            {
                _extended = true;
                _timeSinceLastState = 0f;
                GetComponent<SpriteRenderer>().color = _extendColor;
            }
        }
        if  (_extended)
        {
            CheckHit();
        }
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
                plr.TakeDamage(dmg);
            }
        }
    }
}
