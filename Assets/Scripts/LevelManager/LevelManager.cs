using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Am feelin lazy temp solution just for testing
    [SerializeField] private List<RoomObject> _rooms;

    [SerializeField] GameObject EnvironmentGroup;

    private RoomObject _currentRoom;
    private List<GameObject> _roomsClone;
    private FloorRoom currentFloor;

    public void MoveRoom(DoorDirection dir)
    {
        foreach(GameObject roomObj in _roomsClone)
        {
            roomObj.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _roomsClone = new List<GameObject>();
        foreach (RoomObject room in _rooms)
        {
            GameObject roomObj = Instantiate<GameObject>(room.roomPrefab);
            _roomsClone.Add(roomObj);
            roomObj.SetActive(false);
        }
        _roomsClone[0].SetActive(true);
        _currentRoom = _rooms[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
