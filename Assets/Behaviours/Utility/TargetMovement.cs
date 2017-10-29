using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public Transform move_pos;

    private bool at_move_pos = false;
	
	// Update is called once per frame
	void Update ()
    {
        if (at_move_pos == false)
        {
            MoveToPosition();
        }

        if (at_move_pos == true)
        {
            // Do Speech stuff
        }
    }



    private void MoveToPosition()
    {
        if (transform.position == move_pos.position)
        {
            at_move_pos = true;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, move_pos.position, Time.deltaTime);
    }
}
