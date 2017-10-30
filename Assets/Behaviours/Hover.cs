using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

    public float speed = 90f;
    public float turnSpeed = 5f;
    public float hoverForce = 65f;
    public float hoverHeight = 3.5f;
    private float powerInput;
    private float turnInput;
    [SerializeField]
    GameObject head = null;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        //powerInput = Input.GetAxis("Vertical");
        //turnInput = Input.GetAxis("Horizontal");        
    }

    void FixedUpdate()
    {
        if (head)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(head.transform.position, -transform.up, hoverHeight);

            foreach(RaycastHit2D hit in hits)
            {
                if (hit)
                {
                    if (hit.transform.tag != "Ragdoll")
                    {
                        Debug.Log(hit.transform.tag);

                        float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
                        Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
                        head.GetComponent<Rigidbody2D>().AddForce(appliedHoverForce, ForceMode2D.Force);
                    }
                }
            }            
        }
    }
}
