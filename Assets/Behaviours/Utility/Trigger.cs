using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] UnityEvent trigger_events;


	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}



    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
            return;

        trigger_events.Invoke();
    }
}
