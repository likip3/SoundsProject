using UnityEngine;

public class RoomTrapDoor : MonoBehaviour
{
    private Camera _camera;
    public Transform camPosDown;
    public Transform camPosUp;
    public float camScaleDown;
    public float camScaleUp;

    private void Start()
    {
        _camera = Camera.main;
        if (SettingsTab.renderRooms) return;
        var parent = transform.parent;
        transform.parent = null;
        parent.gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.transform.position.y > transform.position.y)
            {
                _camera.transform.position = camPosUp.position;
                _camera.transform.Translate(0, 0, -10);
                _camera.orthographicSize = camScaleUp;
                col.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
                if (SettingsTab.renderRooms) return;
                camPosDown.parent.gameObject.SetActive(false);
                camPosUp.parent.gameObject.SetActive(true);
            }
            else if (col.transform.position.y < transform.position.y)
            {
                _camera.transform.position = camPosDown.position;
                _camera.transform.Translate(0, 0, -10);
                _camera.orthographicSize = camScaleDown;
                if (SettingsTab.renderRooms) return;
                camPosDown.parent.gameObject.SetActive(true);
                camPosUp.parent.gameObject.SetActive(false);
            }
        }
    }
}