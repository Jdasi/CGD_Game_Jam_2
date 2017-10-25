using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAiming : MonoBehaviour
{
    [SerializeField] Rigidbody2D gun_aimer;
    [SerializeField] HingeJoint2D shoulder_joint;
    [SerializeField] float aim_force = 10;

    private Vector2 target_pos = new Vector2();
    private bool fire = false;


    void Update()
    {
        UpdateTargetPos();
    }


	void FixedUpdate ()
    {
		gun_aimer.AddForce(-target_pos.normalized * aim_force);

        if (Input.GetButton("Fire1"))
            gun_aimer.AddForce(-gun_aimer.transform.forward * 100000000);
	}


    void UpdateTargetPos()
    {
       Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        target_pos = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f) - mousePos;
    }

}
