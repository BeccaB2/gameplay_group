using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class killEnemy : MonoBehaviour {

    // Use this for initialization
    public Transform spawnPoint;
    public GameObject player;
    public string scene1 = "StartIsland_Jake";
    public string scene2 = "MiddlePark_Jake";
    public string scene3 = "BossLevel";
    private string sceneName;
    public GameObject doubleSpeed;
    public GameObject secondPlatforms;
    

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    private void Update()
    {
        if (characterControls.doubleSpeedPickedUp == true)
        {
            doubleSpeed.SetActive(false);
        }
        else if (characterControls.doubleSpeedPickedUp == false)
        {
            doubleSpeed.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (sceneName == scene1)
        {

        }

        if (sceneName == scene2)
        {
            player.transform.position = spawnPoint.transform.position;
            characterControls.doubleSpeedPickedUp = false;
            secondPlatforms.SetActive(true);

        }

        if(sceneName == scene3)
        {
             if (other.tag == "Player")
            {
                characterControls.health = 0;
            }
        }
       
    }
}
