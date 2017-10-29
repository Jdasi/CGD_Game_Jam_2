using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_TYPE
{
    ENEMY_TANK = 0
}

public class Enemy : MonoBehaviour
{
    public bool control;
    public Transform destination;
    private float movement;
    public ENEMY_TYPE EnemyType;
    // Use this for initialization
    void Start()
    {

    }




    // Update is called once per frame
    void Update()
    {
        if (control)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                movement = 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                movement = -1;
            }
        }
        if (destination != null)
        {
            float distance = transform.position.x - destination.position.x;
            if (distance >= 1.5f)
            {
                movement = 1;
            }
            else if (distance <= -1.5f)
            {
                movement = -1;
            }
            else
            {
                movement = 0;
            }
        }
    }

    void FixedUpdate()
    {
        foreach (WheelJoint2D x in GetComponentsInChildren<WheelJoint2D>())
        {
            JointMotor2D motor = new JointMotor2D();
            motor.maxMotorTorque = 10000;
            motor.motorSpeed = movement * 300;
            x.motor = motor;
        }
    }
}
