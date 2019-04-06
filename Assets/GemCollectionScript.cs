using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GemCollectionScript : MonoBehaviour {

    public string scene1;
    public string scene2;
    public string scene3;

    // Checks if all gems are collected in each scene
    public static int gemCountS1 = 6;
    int gemCountS2;
    int gemCountS3;

    //public Transform S1Gems;
    public GameObject[] S1Gems;

    // Use this for initialization
    void Start ()
    {
        //if (gems == null)
        //{
        //    gems = GameObject.FindGameObjectsWithTag("Gem");
        //}

        //foreach (GameObject gem in gems)
        //{
        //    Instantiate(gemPrefab, gem.transform.position, gem.transform.rotation);
        //}

    }

    // Update is called once per frame
    void Update ()
    {
        Debug.Log(gemCountS1);

        // Retrieves current scene
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == scene1)
        {
            // Check if all gems taken
            S1Gems = GameObject.FindGameObjectsWithTag("Gem");
            //gemCountS1 = S1Gems.Length;
        }

        if (sceneName == scene2)
        {
            // Do something...


        }

        if (sceneName == scene3)
        {
            // Do something...
        }

    }
}
