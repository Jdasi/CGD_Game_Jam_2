using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInfo : MonoBehaviour
{

    public static bool gannonAlive = true;
    public static bool ducharmeAlive = true;
    public static bool nabeemAlive = true;

    public Button Mission1Alive;
    public Button Mission2Alive;
    public Button Mission3Alive;

    public Button Mission1Dead;
    public Button Mission2Dead;
    public Button Mission3Dead;

	// Use this for initialization
	void Start ()
    {
        CheckCharacters();
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void CheckCharacters()
    {
        if (gannonAlive == true)
        {
            Mission1Alive.gameObject.SetActive(true);
            Mission1Dead.gameObject.SetActive(false);
        }
        else if (gannonAlive == false)
        {
            Mission1Alive.gameObject.SetActive(false);
            Mission1Dead.gameObject.SetActive(true);
        }
        if (ducharmeAlive == true)
        {
            Mission2Alive.gameObject.SetActive(true);
            Mission2Dead.gameObject.SetActive(false);
        }
        else if (ducharmeAlive == false)
        {
            Mission2Alive.gameObject.SetActive(false);
            Mission2Dead.gameObject.SetActive(true);
        }
        if (nabeemAlive == true)
        {
            Mission3Alive.gameObject.SetActive(true);
            Mission3Dead.gameObject.SetActive(false);
        }
        else if (nabeemAlive == false)
        {
            Mission3Alive.gameObject.SetActive(false);
            Mission3Dead.gameObject.SetActive(true);
        }
    }
}
