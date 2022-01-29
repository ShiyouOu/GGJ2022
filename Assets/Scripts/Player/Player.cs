using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Stats _plrStats;
    private bool _plrAlive = true;

    public Stats plrStats
    {
        get
        {
            return _plrStats;
        }
        set
        {
            
        }
    }

    public void RespawnPlayer()
    {
        if (!_plrAlive)
        {
            _plrStats = new Stats(100);
        }
    }

    public void TakeDamage(int dmg)
    {
        dmg = Mathf.Clamp(dmg, 0, int.MaxValue);

        // So hp doesn't go under 0
        if (dmg >= plrStats.currentHP)
        {
            plrStats.currentHP = 0;
            _plrAlive = false;
            //deathScreen.SetActive(true);
        }
        else
        {
            plrStats.currentHP -= dmg;
        }
    }

    private void Start()
    {
        RespawnPlayer();
    }
}
