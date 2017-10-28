﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GunAiming : MonoBehaviour
{
    [Header("Parameters")]
    public bool can_shoot = true;
    [SerializeField] float aim_force = 10;
    [SerializeField] float recoil_force;

    [Space]
    [SerializeField] float ejection_force;
    [SerializeField] float min_ejection_torque;
    [SerializeField] float max_ejection_torque;

    [Space]
    [SerializeField] float scan_distance;
    [SerializeField] float scan_tolerance;
    [SerializeField] LayerMask ray_hit_layers;
    [SerializeField] LayerMask ray_obstacle_layers;
    [SerializeField] LayerMask cast_hit_layers;
    [SerializeField] WheelchairControl wheelchair;

    [Header("References")]
    [SerializeField] Rigidbody2D gun_aimer;
    [SerializeField] Rigidbody2D gun;
    [SerializeField] Transform ejection_point;
    [SerializeField] GameObject particle_effect;
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bullet_casing_prefab;
    [SerializeField] GameObject bullet_prefab;

    private Vector3 target_pos;
    private bool fire = false;


    void Update()
    {
        UpdateTargetPos();
        fire = can_shoot && Input.GetButtonDown("Fire1");

        if (fire)
        {
            if (!HitScan())
            {
                // Add recoil instantly.
                AddRecoil();
            }

            SpawnParticle();
            SpawnBulletCasing();

            AudioManager.PlayOneShot("gun_shot");
        }
    }


    void AddRecoil()
    {
        gun.AddForce(-gun.transform.right * recoil_force);
    }


    void SpawnParticle()
    {
        var particle = Instantiate(particle_effect);

        particle.transform.position = muzzle.position;
        particle.transform.rotation = Quaternion.LookRotation(gun.transform.right);

        Destroy(particle, 5);
    }


    void SpawnBulletCasing()
    {
        var casing_clone = Instantiate(bullet_casing_prefab, ejection_point.position, gun.transform.rotation);
        Rigidbody2D casing_body = casing_clone.GetComponent<Rigidbody2D>();

        casing_body.AddForce(gun.transform.right * ejection_force);
        casing_body.AddTorque(Random.Range(min_ejection_torque, max_ejection_torque));
    }


    bool HitScan()
    {
        bool hit_ragdoll = false;
        // Try direct ray.
        List<RaycastHit2D> ray = ShootRayFromGun(gun.transform.right, Color.green, ref hit_ragdoll);

        for(int i = 0; i < ray.Count; i++)
        {
            if (ray[i].rigidbody == null)
            {
                /*
                // Assit player with circle cast.
                var cast = Physics2D.CircleCast(muzzle.position, scan_tolerance,
                    gun.transform.right, scan_distance, cast_hit_layers);

                if (cast.rigidbody == null)
                    return false;

                // Try to hit with new ray.
                Vector3 dir = ((Vector3)cast.rigidbody.position - muzzle.position).normalized;
                ray = ShootRayFromGun(dir, Color.yellow);

                if (ray[i].rigidbody == null)
                    return false;*/
            }
            //Debug.Log(ray[i].rigidbody.name);

            if (hit_ragdoll == true)
            {
                StartCoroutine(BulletCamSequence(ray[i]));
            }

            else
                ShootRound();
        }

        return true;
    }


    List<RaycastHit2D> ShootRayFromGun(Vector3 _dir, Color _color, ref bool _hit_ragdoll)
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        RaycastHit2D[] ray = Physics2D.RaycastAll(muzzle.position, _dir, scan_distance, ray_hit_layers);
        Debug.DrawLine(muzzle.position, muzzle.position + (gun.transform.right * scan_distance), _color, 3);

        for (int i = 0; i < ray.Length; i++)
        {
            Vector3 target = ray[i].point;
            Vector3 dir_to_target = (target - muzzle.position).normalized;
            float distance_to_target = Vector2.Distance(muzzle.position, target);

            if (!Physics2D.Raycast(muzzle.position, dir_to_target, distance_to_target, ray_obstacle_layers))
            {
                Debug.Log("We can hit this target");
                hits.Add(ray[i]);
            }

            // to enable Bullet cam, only if Ragdoll hit
            if (ray[i].collider.gameObject.tag == "Ragdoll")
            {
                _hit_ragdoll = true;
            }
        }

        return hits;
    }



    void ShootRound()
    {
        var clone = Instantiate(bullet_prefab, muzzle.position,
            gun.transform.rotation);
    }


    IEnumerator BulletCamSequence(RaycastHit2D _hit)
    {
        CameraManager cam = GameManager.scene.camera_manager;
        CameraSettings cam_settings = GameManager.scene.camera_manager.GetSettings();

        var clone = Instantiate(bullet_prefab, muzzle.position,
            gun.transform.rotation);

        Bullet bullet = clone.GetComponent<Bullet>();
        bullet.Init((Vector3)_hit.point - muzzle.position, _hit);

        SloMoManager.bullet_time = true;
        can_shoot = false;

        cam.SetTarget(bullet.transform, 10);
        cam.update_mode = CameraUpdateMode.DELTA;

        yield return new WaitUntil(() => bullet.trajectory_complete);

        SloMoManager.bullet_time = false;
        AddRecoil();

        yield return new WaitForSeconds(1);

        cam.SetSettings(cam_settings);
        can_shoot = true;
    }


	void FixedUpdate()
    {
        Vector3 gun_force = (target_pos - gun.transform.position).normalized * aim_force;
		gun_aimer.AddForce(gun_force);

        if (wheelchair != null)
            wheelchair.rigid_body.AddForce(-gun_force);
    }


    void UpdateTargetPos()
    {
        target_pos = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);
    }

}
