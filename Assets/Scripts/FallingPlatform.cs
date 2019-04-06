using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float duration = 0.5f;
    public float counter = 0;

    public float speed = 2;

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
            StartCoroutine(Fade());
        }

    }

    IEnumerator Fade()
    {
        Renderer platform = GetComponent<Renderer>();
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

        if(platform.material.color.a == 0)
        {
            platform.transform.gameObject.SetActive(false);
        }
    }
}
