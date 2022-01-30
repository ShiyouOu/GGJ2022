using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootField
{
    public GameObject drop;
    public int minAmount;
    public int maxAmount;
    public int chanceNum = 1;
    public int chanceDenom = 100;
}