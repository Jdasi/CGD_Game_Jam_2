using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public SceneManager scene_manager;

    private bool message_sent;

	// Use this for initialization
	void Start ()
    {
        message_sent = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}



    void OnTriggerEnter2D(Collider2D other)
    {
        if(message_sent == false)
        {
            // Call only needs to be made once
            if (other.gameObject.tag == "Player")
            {
                scene_manager.PlayerInPosition();
                message_sent = true;
            }
        }
    }
}
