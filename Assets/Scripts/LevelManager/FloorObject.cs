using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Floor", menuName = "Room System/Floor")]
public class FloorObject : ScriptableObject
{
    public FloorRoom[] rooms;
}