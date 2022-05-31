using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    private Camera _camera;
    public Transform camPosLeft;
    [SerializeField] private Transform camPosRight;
    public float camScaleLeft;
    [SerializeField] private float camScaleRight;
    [SerializeField] private bool simple;

    private void Start()
    {
        _camera = Camera.main;
        if (simple) return;
        camScaleRight = GetComponentInParent<RoomInitializator>().MyCamScale;
        camPosRight = GetComponentInParent<RoomInitializator>().MyCamPos;
        if (SettingsTab.renderRooms) return;
        var temp = transform.parent;
        transform.parent = null;
        temp.gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (col.transform.position.x < transform.position.x)
        {
            _camera.transform.position = camPosLeft.position;
            _camera.transform.Translate(0, 0, -10);
            _camera.orthographicSize = camScaleLeft;
            col.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000, 0));
            if (SettingsTab.renderRooms) return;
            camPosLeft.parent.gameObject.SetActive(true);
            camPosRight.parent.gameObject.SetActive(false);
        }
        else if (col.transform.position.x > transform.position.x)
        {
            _camera.transform.position = camPosRight.position;
            _camera.transform.Translate(0, 0, -10);
            _camera.orthographicSize = camScaleRight;
            col.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000, 0));
            if (SettingsTab.renderRooms) return;
            camPosRight.parent.gameObject.SetActive(true);
            camPosLeft.parent.gameObject.SetActive(false);
        }
    }
}