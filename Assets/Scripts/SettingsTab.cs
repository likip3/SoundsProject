using UnityEngine;
using UnityEngine.UI;

public class SettingsTab : MonoBehaviour
{
    public static bool renderRooms;
    private bool backGround;
    [SerializeField] private GameObject DoneReset;
    [SerializeField] private GameObject SurePanel;
    [SerializeField] private GameObject DrawRoomPanel;


    private void Start()
    {
        if (PlayerPrefs.HasKey("RoomRender")) renderRooms = PlayerPrefs.GetInt("RoomRender") == 1;

        DrawRoomPanel.GetComponent<Toggle>().isOn = renderRooms;
    }
    public void Sure()
    {
        SurePanel.SetActive(!SurePanel.activeInHierarchy);
    }

    public void ResetProgress()
    {
        if (!DoneReset.activeInHierarchy)
            DoneReset.SetActive(true);
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void CloseSure()
    {
        if (SurePanel.activeInHierarchy)
            SurePanel.SetActive(false);
        if (DoneReset.activeInHierarchy)
            DoneReset.SetActive(false);
    }

    public void BackgroundChange()
    {
        Camera.main.backgroundColor = backGround ? new Color(0, 0, 0) : new Color(0.1921569f, 0.3019608f, 0.4745098f);
        backGround = !backGround;
    }

    public void RenderRooms()
    {
        renderRooms = !renderRooms;
        PlayerPrefs.SetInt("RoomRender",renderRooms ? 1 : 0);
    }
}