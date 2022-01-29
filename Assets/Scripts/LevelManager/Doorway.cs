using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorDirection
{
    Up,
    Down,
    Left,
    Right
}

[System.Serializable]
public class Doorway
{
    public DoorDirection direction;
}
