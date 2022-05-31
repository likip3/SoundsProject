using UnityEngine;

public class GlobalLevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] AdditionLowerRooms;
    [SerializeField] private GameObject[] AdditionUpperRooms;
    [SerializeField] private GameObject[] AllRooms;
    [SerializeField] private GameObject EndRoom;


    public int levelLength;
    private int seed;

    private void Start()
    {
        var temp = Random.Range(1000, 9999999);
        if (!PlayerPrefs.HasKey("GameSaved"))
        {
            Random.InitState(temp);
            PlayerPrefs.SetInt("seed", temp);
        }
        else
        {
            Random.InitState(PlayerPrefs.GetInt("seed"));
        }


        while (levelLength > 0)
            GenerateRoom(AllRooms, RoomPoint.position);
        var newRoom = Instantiate(EndRoom, RoomPoint.position, new Quaternion());
        newRoom.GetComponentInChildren<RoomDoor>().camPosLeft = currentRoom.GetComponent<RoomInitializator>().MyCamPos;
        newRoom.GetComponentInChildren<RoomDoor>().camScaleLeft =
            currentRoom.GetComponent<RoomInitializator>().MyCamScale;
        newRoom.GetComponent<RoomInitializator>().lowerRooms = AdditionLowerRooms;
        newRoom.GetComponent<RoomInitializator>().upperRooms = AdditionUpperRooms;
        ChangeRoom(newRoom);

    }

    private void ChangeRoom(GameObject room)
    {
        currentRoom = room;
        RoomPoint = currentRoom.GetComponent<RoomInitializator>().RoomPoint;
    }

    private void GenerateRoom(GameObject[] rooms, Vector3 point)
    {
        var roomId = Random.Range(0, rooms.Length);
        var newRoom = Instantiate(rooms[roomId], point, new Quaternion());
        newRoom.GetComponentInChildren<RoomDoor>().camPosLeft = currentRoom.GetComponent<RoomInitializator>().MyCamPos;
        newRoom.GetComponentInChildren<RoomDoor>().camScaleLeft =
            currentRoom.GetComponent<RoomInitializator>().MyCamScale;
        newRoom.GetComponent<RoomInitializator>().lowerRooms = AdditionLowerRooms;
        newRoom.GetComponent<RoomInitializator>().upperRooms = AdditionUpperRooms;
        ChangeRoom(newRoom);
        levelLength--;
    }

    #region RunTimeParams

    [SerializeField] private GameObject currentRoom;

    [SerializeField] private Transform RoomPoint;
    //private Transform MediumRoomPoint;
    //private Transform LargeRoomPoint;

    #endregion
}