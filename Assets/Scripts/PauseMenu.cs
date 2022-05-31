using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused;
    [SerializeField] private GameObject Monster;
    public GameObject pauseMenuUI;
    [SerializeField] private GameObject SettingsTab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        SettingsTab.GetComponent<Animator>()
            .SetBool("Settingsed",
                !SettingsTab.GetComponent<Animator>()
                    .GetBool("Settingsed"));
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("xPos", Monster.transform.position.x);
        PlayerPrefs.SetFloat("yPos", Monster.transform.position.y);

        var healthSys = Monster.GetComponent<HealthSystem>();
        PlayerPrefs.SetFloat("maxHealth", healthSys.maxHealth);
        PlayerPrefs.SetFloat("regeneration", healthSys.regeneration);
        PlayerPrefs.SetFloat("damageReduce", healthSys.damageReduce);

        var soundSys = Monster.GetComponent<SoundsSystem>();
        PlayerPrefs.SetFloat("damage", soundSys.damage);
        PlayerPrefs.SetFloat("cooldown", soundSys.cooldown);
        PlayerPrefs.SetFloat("particleSpeed", soundSys.particleSpeed  );
        PlayerPrefs.SetInt("attackAngel", soundSys.attackAngel  );
        PlayerPrefs.SetInt("lifePunches", soundSys.lifePunches  );
        PlayerPrefs.SetFloat("particleCount", soundSys.particleCount);
        var tentacleSys = Monster.GetComponentsInChildren<Tentacle>();
        foreach (var tentacle in tentacleSys)
        {
            PlayerPrefs.SetFloat("tentacleStrength", tentacle.tentacleStrength);
            PlayerPrefs.SetFloat("raycastDistance", tentacle.raycastDistance);
        }

        PlayerPrefs.SetInt("GameSaved",1);
    }

    public void Restart()
    {
        var temp = PlayerPrefs.GetInt("RoomRender");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("tutorial", 1);
        PlayerPrefs.SetInt("RoomRender", temp);
        SceneManager.LoadScene("Tutorial");
        Resume();
    }
}