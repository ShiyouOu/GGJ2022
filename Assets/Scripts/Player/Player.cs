using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacingDirection
{
    Up,
    Down,
    Right,
    Left,
}

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] int startHealth;
    [SerializeField] private float _iframeTime = 1f;
    private float _timeSinceLastHit = 0f;

    private Stats _plrStats;
    private bool _plrAlive = true;
    private FacingDirection _plrDirection = FacingDirection.Down;

    private void Start()
    {
        instance = this;
        _plrStats = new Stats(startHealth);
        PlayerHealthbar.instance.UpdateHealth();
    }

    private void Update()
    {
        _timeSinceLastHit += Time.deltaTime;
    }

    public void TakeDamage(int dmg)
    {
        // Iframe
        if(_timeSinceLastHit < _iframeTime) { return; }

        dmg = Mathf.Clamp(dmg, 0, int.MaxValue);

        // So hp doesn't go under 0
        if (dmg >= _plrStats.currentHP)
        {
            _plrStats.currentHP = 0;
            _plrAlive = false;
            //deathScreen.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            _plrStats.currentHP -= dmg;
        }

        _timeSinceLastHit = 0f;
        // Update the Healthbar UI
        PlayerHealthbar.instance.UpdateHealth();
    }

    public int GetHealth()
    {
        return _plrStats.currentHP;
    }
}
