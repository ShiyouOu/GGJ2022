using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour, IDamageable
{
    public AudioClip clip;

    [SerializeField] private int _maxDurability = 5;
    [SerializeField] private Loot _loot;

    private Collider2D _collider;
    private int _currentDurability;
    private bool _destroyed = false;

    private bool TestLuck(int num, int num2)
    {
        int randomNumber = Random.Range(num, num2);
        if (randomNumber <= num)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RewardPlayer()
    {
        Player.instance.AddKarma(_loot.karma);
        foreach (LootField loot in _loot.lootTable)
        {
            if (TestLuck(loot.chanceNum, loot.chanceDenom))
            {
                GameObject newObj = Instantiate<GameObject>(loot.drop.gameObject);
                Rigidbody rb = newObj.AddComponent<Rigidbody>();
                BoxCollider col = newObj.AddComponent<BoxCollider>();
                newObj.transform.position = gameObject.transform.position;
                newObj.transform.SetParent(LevelManager.instance.GetActiveFloor().transform);
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        print("hit " + gameObject.name);
        if (!_destroyed)
        {
            if (dmg >= _currentDurability)
            {
                _destroyed = true;
                _currentDurability = 0;
                RewardPlayer();
                Destroy(gameObject);
                SoundManager.Instance.PlayEffect(clip, transform.position);
            }
            else
            {
                _currentDurability -= dmg;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _currentDurability = _maxDurability;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
