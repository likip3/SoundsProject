using UnityEngine;

#pragma warning disable CS0649

public class RoomInitializator : MonoBehaviour
{
    [SerializeField] private bool isLowerExit;

    [SerializeField] private bool isUpperExit;
    [SerializeField] private Transform lowerPoint;
    public GameObject[] lowerRooms;
    public Transform MyCamPos;
    public float MyCamScale;
    public Transform RoomPoint;
    [SerializeField] private Transform upperPoint;

    public GameObject[] upperRooms;


    private void Start()
    {
        if (isUpperExit)
        {
            var roomId = Random.Range(0, upperRooms.Length);
            var newRoom = Instantiate(upperRooms[roomId], upperPoint.position, new Quaternion());
            newRoom.GetComponentInChildren<RoomTrapDoor>().camPosDown = MyCamPos;
            newRoom.GetComponentInChildren<RoomTrapDoor>().camScaleDown = MyCamScale;
            newRoom.GetComponent<RoomInitializator>().upperRooms = upperRooms;
        }

        if (isLowerExit)
        {
            var roomId = Random.Range(0, lowerRooms.Length);
            var newRoom = Instantiate(lowerRooms[roomId], lowerPoint.position, new Quaternion());
            newRoom.GetComponentInChildren<RoomTrapDoor>().camPosUp = MyCamPos;
            newRoom.GetComponentInChildren<RoomTrapDoor>().camScaleUp = MyCamScale;
            newRoom.GetComponent<RoomInitializator>().lowerRooms = lowerRooms;
        }
    }
}