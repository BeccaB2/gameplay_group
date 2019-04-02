using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStats : MonoBehaviour {

    characterControls stats;
    ChangeScene getStats;
	// Use this for initialization
	void Start ()
    {
        stats = GameObject.Find("Mage").GetComponent<characterControls>();
        getStats = GameObject.Find("SceneManager").GetComponent<ChangeScene>();

        stats.keyCollected = getStats.keyCollected;
        stats.weaponCollected = getStats.weaponCollected;
        stats.doubleJumpActive = getStats.doubleJumpActive;
        stats.doubleSpeedActive = getStats.doubleSpeedActive;
        stats.doubleStrengthActive = getStats.doubleStrengthActive;
        stats.score = getStats.score;
        stats.health = getStats.health;
    }
	

}
