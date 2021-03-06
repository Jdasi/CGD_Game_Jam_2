﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool able_to_shoot;
    public Transform SpawnPosition;
    public Rigidbody2D projectile;
    public float force = 1000;
    private float timer = 0.0f;
    public float shoot_delay = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!able_to_shoot)
        {
            timer += Time.deltaTime;
            if (timer > shoot_delay)
            {
                able_to_shoot = true;
            }
        }

    }

    public void Shoot(DIRECTION_X direction)
    {
        // if able to shoot
        if (!able_to_shoot) return;

        AudioManager.PlayOneShot("TankShot");

        Rigidbody2D shot = Instantiate(projectile, SpawnPosition.position, SpawnPosition.rotation) as Rigidbody2D;
        shot.AddForce((int)direction * SpawnPosition.right * force);
        if (direction == DIRECTION_X.RIGHT)
            projectile.GetComponent<SpriteRenderer>().flipX = true;
        able_to_shoot = false;
        timer = 0.0f;
    }
}
