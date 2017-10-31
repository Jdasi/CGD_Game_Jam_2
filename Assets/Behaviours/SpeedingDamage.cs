using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedingDamage : MonoBehaviour {

    private PlayerStatus status;

	// Use this for initialization
	void Start () {
        status = GetComponentInParent<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D c)
    {
        float speed = c.relativeVelocity.magnitude;

        if (speed > 20)
        {
            if (c.collider.gameObject.layer == 0)
            {
                status.Damage((int)speed / 5);
            }
        }
    }
}
