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

    private void Update()
    {
        foreach(DoorInfo door in doors)
        {
            if (CheckHit(door.door))
            {
                LevelManager.instance.MoveRoom(door.direction);
            }
        }
    }

    private bool CheckHit(GameObject door)
    {
        Collider2D[] results = new Collider2D[5];
        Collider2D collider = door.GetComponent<Collider2D>();
        collider.OverlapCollider(new ContactFilter2D(), results);

        foreach (Collider2D col in results)
        {
            if (!col) { break; }
            Player playerTouched = col.gameObject.GetComponent<Player>();
            if (playerTouched)
            {
                return true;
            }
        }

        return false;
    }
}
