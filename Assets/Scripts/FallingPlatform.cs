using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public static float duration = 1.5f;
    public static float counter = 0;

    public static float speed = 0.5f;

    Renderer platform;

    // Use this for initialization
    void Start ()
    {
        platform = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		// Check if player respawned / set platforms back?
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fade());
        }

    }

    IEnumerator Fade()
    {
        float diffAlpha = (0 - platform.material.color.a);

        int targetAlpha = 0;

        while (counter < duration)
        {
            float alphaAmount = platform.material.color.a + (Time.deltaTime * diffAlpha) / duration;
            platform.material.color = new Color(platform.material.color.r, platform.material.color.g, platform.material.color.b, alphaAmount);

            counter += Time.deltaTime * speed;
            yield return null;
        }

        platform.material.color = new Color(platform.material.color.r, platform.material.color.g, platform.material.color.b, targetAlpha);

        if(platform.material.color.a <= 200)
        {
            platform.transform.gameObject.SetActive(false);
        }
    }
}
