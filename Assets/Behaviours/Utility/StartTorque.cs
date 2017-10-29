using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StartTorque : MonoBehaviour
{
    [SerializeField] private float torque;


	void Start ()
    {
		GetComponent<Rigidbody2D>().AddTorque(torque);
	}

}
