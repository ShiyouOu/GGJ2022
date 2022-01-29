using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Floor Room", menuName = "Room System/Floor Room")]
public class FloorRoom : ScriptableObject
{
    public RoomTemplate template;
    public RoomLink[] links;
}

[System.Serializable]
public class RoomLink
{
    public DoorDirection direction;
    public FloorRoom floorRoom;
}
