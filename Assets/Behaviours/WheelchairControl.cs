using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairControl : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float speed_forward;
    [SerializeField] float speed_back;

    [Space]
    [SerializeField] float torque_forward;
    [SerializeField] float torque_back;

    [Header("References")]
    [SerializeField] WheelJoint2D front_wheel;
    [SerializeField] WheelJoint2D back_wheel;

    private JointMotor2D motor_front;
    private JointMotor2D motor_back;

    private float current_speed;


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;

        motor_front.motorSpeed = speed_forward * horizontal;
        motor_front.maxMotorTorque = torque_forward;
        front_wheel.motor = motor_front;

        motor_back.motorSpeed = speed_forward * horizontal;
        motor_back.maxMotorTorque = torque_forward;
        back_wheel.motor = motor_back;
    }


    public void FixedUpdate()
    {

    }

}
