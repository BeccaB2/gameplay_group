using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string scene;

    public GameObject player;
    public Vector3 spawnPoint = new Vector3(32.32f,0.67f,-21.3f);
    characterControls stats;

    public bool keyCollected;
    public bool weaponCollected;
    public bool doubleJumpActive;
    public bool doubleSpeedActive;
    public bool doubleStrengthActive;
    public int score;
    public float health;

    // Use this for initialization
    void Start ()
    {
        stats = GameObject.Find("Mage").GetComponent<characterControls>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIT");
            Application.LoadLevel(scene);
            keyCollected = stats.keyCollected;
            weaponCollected = stats.weaponCollected;
            doubleJumpActive = stats.doubleJumpActive;
            doubleSpeedActive = stats.doubleSpeedActive;
            doubleStrengthActive = stats.doubleStrengthActive;
            score = stats.score;
            health = stats.health;
        }

    }
}

