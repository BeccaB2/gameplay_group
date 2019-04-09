using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collectGem : MonoBehaviour
{
    // Check scene
    public string scene1;
    public string scene2;

    // Use this for initialization
    void Start ()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == scene1)
        {
            if(GemCollectionScript.gemCountS1 < 12)
            {
                Destroy(gameObject);
            }
        }

        if (sceneName == scene2)
        {
            if (GemCollectionScript.gemCountS2 < 26)
            {
                Destroy(gameObject);
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {

    }
}
