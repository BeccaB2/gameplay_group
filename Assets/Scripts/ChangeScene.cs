using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string scene;

    //public GameObject player;
   // public Vector3 spawnPoint = new Vector3(32.32f,0.67f,-21.3f);

    // Use this for initialization
    void Start ()
    {
    
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("HIT");
            //Application.LoadLevel(scene);
            //SceneManager.LoadScene(sceneName: scene);
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }

    }
}

