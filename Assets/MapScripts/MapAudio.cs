using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAudio : MonoBehaviour
{

    public AudioSource audio; //Create an audiosource component on the button and drag it here
    public AudioClip buttonSound; //Put sound clip here

    public bool inRange;

    void Start()
    {
        inRange = false;
    }

    //I don't know how you want to trigger the sound, but here's a basic way
    void Update()
    {

        if (inRange == true)
        {
            audio.PlayOneShot(buttonSound);
            inRange = false;
        }

    }
}