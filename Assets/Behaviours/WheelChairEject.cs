using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChairEject : MonoBehaviour
{
    [SerializeField] HingeJoint2D hinge_joint;


	void Start ()
    {
		
	}
	


	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space))
            Destroy(hinge_joint);
	}
}
