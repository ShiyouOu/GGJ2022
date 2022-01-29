using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    StartRoom,
    SecretRoom,
    BossRoom,
    ItemRoom,
    NormalRoom,
}

[CreateAssetMenu(fileName = "New Room", menuName = "Room System/Room")]
public class RoomObject : ScriptableObject
{
    public GameObject roomPrefab;
    public RoomType roomType = RoomType.NormalRoom;
    public Doorway[] doors;
}
