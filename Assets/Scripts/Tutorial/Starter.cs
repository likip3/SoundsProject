using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private UserUI userUi;
    void Awake()
    {
        if (PlayerPrefs.HasKey("GameSaved"))
            userUi.transform.position = new Vector3(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"));

        userUi.ShowMessage("The Sounds Project", "5 дней до инцидента");
        if (!PlayerPrefs.HasKey("tutorial") && !PlayerPrefs.HasKey("GameSaved"))
        {
            userUi.transform.position = transform.position;
            PlayerPrefs.SetInt("tutorial",1);
        }

        if (PlayerPrefs.HasKey("GameSaved"))
            LoadData();
    }

    void FixedUpdate()
    {

    }

    private void LoadData()
    {
        //userUi.transform.position.x = PlayerPrefs.GetFloat("xPos");
        //userUi.transform.position.y = PlayerPrefs.GetFloat("yPos");

        Random.InitState(PlayerPrefs.GetInt("seed"));

        var healthSys = userUi.GetComponent<HealthSystem>();
        healthSys.maxHealth = PlayerPrefs.GetFloat("maxHealth");
        healthSys.regeneration = PlayerPrefs.GetFloat("regeneration");
        healthSys.damageReduce = PlayerPrefs.GetFloat("damageReduce");
        var soundSys = userUi.GetComponent<SoundsSystem>();
        soundSys.damage = PlayerPrefs.GetFloat("damage");
        soundSys.cooldown = PlayerPrefs.GetFloat("cooldown");
        soundSys.particleSpeed = PlayerPrefs.GetFloat("particleSpeed");
        soundSys.attackAngel = PlayerPrefs.GetInt("attackAngel");
        soundSys.lifePunches = PlayerPrefs.GetInt("lifePunches");
        soundSys.particleCount = PlayerPrefs.GetInt("particleCount");
        var tentacleSys = userUi.GetComponentsInChildren<Tentacle>();
        foreach (var tentacle in tentacleSys)
        {
            tentacle.tentacleStrength = PlayerPrefs.GetFloat("tentacleStrength");
            tentacle.raycastDistance = PlayerPrefs.GetFloat("raycastDistance");
        }
    }
}
