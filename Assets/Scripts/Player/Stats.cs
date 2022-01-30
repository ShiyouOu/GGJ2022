using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    private int _maxHP = 100;
    private int _currentHP = 100;
    private int _armor = 0;

    // Getters and Setters
    public int maxHP
    {
        get
        {
            return _maxHP;
        }
        set
        {
            _maxHP = value;
        }
    }
    public int currentHP
    {
        get
        {
            return _currentHP;
        }
        set
        {
            if(value > maxHP)
            {
                _currentHP = maxHP;
            }
            else
            {
                _currentHP = value;
            }
        }
    }
    public int armor
    {
        get
        {
            return _armor;
        }
        set
        {
            _armor = value;
        }
    }

    // Constructors
    public Stats(int hp)
    {
        _maxHP = hp;
        _currentHP = hp;
    }

    public Stats(int hp, bool basePlayer)
    {
        _maxHP = hp;
        _currentHP = hp;
        if (basePlayer)
        {
            _armor = 0;
        }
    }
}