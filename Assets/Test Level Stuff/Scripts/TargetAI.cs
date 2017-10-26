using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAI : MonoBehaviour
{
    public Transform spawn_pos;
    private Vector3 speech_pos;

    private bool at_target_pos;
    private int health = 1;


	// Use this for initialization
	void Start ()
    {
        at_target_pos = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (at_target_pos == false)
        {
            MoveToTarget();
        }

        if (at_target_pos == true)
        {
            // Do Speech stuff
        }
    }



    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, speech_pos, Time.deltaTime);
    }



    public void SetTarget(Vector3 _position)
    {
        speech_pos = _position;
    }
}
