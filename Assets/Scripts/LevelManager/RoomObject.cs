using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "Room System/Room")]
public class RoomObject : ScriptableObject
{
    public GameObject roomPrefab;
    public RoomTemplate template;
}
