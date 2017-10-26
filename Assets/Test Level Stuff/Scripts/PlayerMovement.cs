using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Script just for Testing!!!!!
    public float speed = 10.0f;
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey("w"))
        {
            GetComponent<Rigidbody>().position += (Vector3.up * Time.deltaTime * speed);
        }

        if (Input.GetKey("s"))
        {
            GetComponent<Rigidbody>().position += (-Vector3.up * Time.deltaTime * speed);
        }

        if (Input.GetKey("a"))
        {
            GetComponent<Rigidbody>().position += (-Vector3.right * Time.deltaTime * speed);
        }

        if (Input.GetKey("d"))
        {
            GetComponent<Rigidbody>().position += (Vector3.right * Time.deltaTime * speed);
        }
    }
}
