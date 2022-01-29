using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DoorInfo
{
    public DoorDirection direction;
    public GameObject door;
    public GameObject spawnLocation;
}


public class RoomInfo : MonoBehaviour
{
    public DoorInfo[] doors;
}
