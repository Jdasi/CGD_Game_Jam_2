using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StartForce : MonoBehaviour
{
    [SerializeField] private Vector2 force;


	void Start ()
    {
		GetComponent<Rigidbody2D>().AddForce(force);
	}
}
