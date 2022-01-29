using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room Template", menuName = "Room System/Room Template")]
public class RoomTemplate : ScriptableObject
{
    public Doorway[] doors;
}
