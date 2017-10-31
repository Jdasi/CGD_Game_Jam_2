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
    public float flipDelay = 1.0f;
    public List<Transform> destination_list;
    private Transform destination;
    int current_dest = 0;
    private float movement;
    private float prev_movement;
    public ENEMY_TYPE EnemyType;
    public Transform WheelRotation;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> spriteList;
    private float frameTimer;
    private float flipTimer = 0.0f;
    private int current_sprite = 0;
    private Turret tank_turret;
    public bool flip;
    private bool movement_active = true;
    // Use this for initialization
    void Start()
    {
        destination = destination_list[current_dest];
        tank_turret = GetComponentInChildren<Turret>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }




    // Update is called once per frame
    void Update()
    {
        if (flip)
        {
            ChangeDirection();
            flip = false;
        }
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
        if (destination != null && movement_active)
        {
            prev_movement = movement;
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
                current_dest++;
                if (current_dest >= destination_list.Count)
                {
                    current_dest = 0;
                }
                destination = destination_list[current_dest];
            }

            if (prev_movement != movement && prev_movement != 0)
            {
                movement_active = false;
            }
            frameTimer += Time.deltaTime;
            //Debug.Log(GetComponent<Rigidbody2D>().velocity.magnitude);
            if (movement != 0 && frameTimer >= 0.5 / GetComponent<Rigidbody2D>().velocity.magnitude)
            {
                frameTimer = 0;
                spriteRenderer.sprite = spriteList[current_sprite];
                current_sprite++;
                if (current_sprite >= spriteList.Count)
                {
                    current_sprite = 0;
                }
            }
        }
        if (!movement_active)
        {
            flipTimer += Time.deltaTime;
            if (flipTimer > flipDelay)
            {
                flipTimer = 0;
                ChangeDirection();
                movement_active = true;
            }
        }
    }

    void ChangeDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        if (tank_turret.direction == DIRECTION_X.LEFT)
        {
            tank_turret.direction = DIRECTION_X.RIGHT;
        }
        else
        {
            tank_turret.direction = DIRECTION_X.LEFT;
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
