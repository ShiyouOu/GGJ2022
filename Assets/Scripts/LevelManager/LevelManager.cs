using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    // Am feelin lazy temp solution just for testing
    [SerializeField] private List<FloorObject> _floors;
    [SerializeField] private List<RoomObject> _roomsPool;
    [SerializeField] private GameObject _nextLevelScreen;
    [SerializeField] private TextMeshProUGUI _levelLabel;

    [SerializeField] GameObject EnvironmentGroup;

    private int _currentFloorLevel = 0;

    private List<RoomObject> _roomObjects;
    private List<GameObject> _roomsClone;
    private int _currentRoomIndex = 0;
    private bool _movingRooms = false;

    public void MoveRoom(DoorDirection dir)
    {
        // So it only triggers once at a time
        if (!_movingRooms)
        {
            _movingRooms = true;
            FloorRoom newFloorRoom = GetFloorRoomByDirection(_floors[_currentFloorLevel].rooms[_currentRoomIndex].links, dir);
            int newRoomIndex = GetRoomIndexByFloorRoom(newFloorRoom);

            // Position the player correctly after newRoom
            RoomInfo roomInfo = _roomsClone[newRoomIndex].GetComponent<RoomInfo>();
            if (roomInfo)
            {
                DoorDirection doorDirection;

                // Get which door we will leave from after entering new room
                if (dir == DoorDirection.Up)
                {
                    doorDirection = DoorDirection.Down;
                }
                else if(dir == DoorDirection.Left)
                {
                    doorDirection = DoorDirection.Right;
                }
                else if(dir == DoorDirection.Right)
                {
                    doorDirection = DoorDirection.Left;
                }
                else
                {
                    doorDirection = DoorDirection.Up;
                }
                foreach (DoorInfo doorInfo in roomInfo.doors)
                {
                    if (doorInfo.direction == doorDirection)
                    {
                        GameObject.FindGameObjectWithTag("Player").transform.position = doorInfo.spawnLocation.transform.position;
                    }
                }
            }
            else
            {
                print("no room info");
                GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 12);
            }

            // Display the new room
            _roomsClone[_currentRoomIndex].SetActive(false);
            _roomsClone[newRoomIndex].SetActive(true);
            _currentRoomIndex = newRoomIndex;
            _movingRooms = false;
        }
    }

    // Find the next room based on current room and the direction we wish to move in
    private FloorRoom GetFloorRoomByDirection(RoomLink[] roomLinks, DoorDirection dir)
    {
        foreach(RoomLink link in roomLinks)
        {
            if (link.direction == dir)
            {
                return link.floorRoom;
            }
        }
        return null;
    }


    // Get the room's index on the floor
    private int GetRoomIndexByFloorRoom(FloorRoom fRoom)
    {
        int count = 0;
        foreach(FloorRoom floorRoom in _floors[_currentFloorLevel].rooms)
        {
            if(floorRoom == fRoom)
            {
                return count;
            }
            count++;
        }
        return 0;
    }

    // Returns a random Room object based on a template
    private RoomObject GetRandomRoomByType(RoomTemplate template)
    {
        List<RoomObject> roomsTypePool = new List<RoomObject>();
        foreach (RoomObject roomObj in _roomsPool)
        {
            if(roomObj.template == template)
            {
                roomsTypePool.Add(roomObj);
            }
        }
        RoomObject randomRoom = roomsTypePool[Random.Range(0, roomsTypePool.Count)];
        return randomRoom;
    }

    private void ClearRooms()
    {
        if(_roomsClone == null){ return; }
        foreach(GameObject roomObj in _roomsClone)
        {
            Destroy(roomObj);
        }
    }

    public void LoadFloor()
    {
        ClearRooms();
        _roomObjects = new List<RoomObject>();
        _roomsClone = new List<GameObject>();

        // Picks random rooms for each section of the floor
        foreach (FloorRoom floorRoom in _floors[_currentFloorLevel].rooms)
        {
            RoomObject randomRoom = GetRandomRoomByType(floorRoom.template);
            _roomObjects.Add(randomRoom);
        }

        // Create new instances of the rooms
        foreach (RoomObject room in _roomObjects)
        {
            GameObject roomObj = Instantiate<GameObject>(room.roomPrefab);
            _roomsClone.Add(roomObj);
            roomObj.SetActive(false);
        }
        // Set the main floor to active
        _roomsClone[0].SetActive(true);
        if(_currentFloorLevel == 0)
        {
            Player.instance.RespawnPlayer();
        }
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 12);
        _currentRoomIndex = 0;
        _levelLabel.SetText("Level: " + (_currentFloorLevel + 1));
    }

    public void NextFloor()
    {
        _nextLevelScreen.SetActive(false);
        if (_currentFloorLevel < _floors.Count-1)
        {
            _currentFloorLevel++;
            LoadFloor();
        }
        else
        {
            LoadFloor();
        }
    }

    public void SetFloorLevel(int num)
    {
        _currentFloorLevel = num;
    }

    public GameObject GetActiveFloor()
    {
        return _roomsClone[_currentRoomIndex];
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadFloor();
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentRoomIndex == _roomObjects.Count - 1)
        {
            BasicEnemy enemy = _roomsClone[_currentRoomIndex].GetComponentInChildren<BasicEnemy>();
            if (!enemy)
            {
                if (!_nextLevelScreen.activeSelf)
                {
                    _nextLevelScreen.SetActive(true);
                }
            }
        }
    }
}
