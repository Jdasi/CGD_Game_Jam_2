using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Header("References")]
    [SerializeField] Rigidbody2D gun_aimer;
    [SerializeField] Rigidbody2D gun;
    [SerializeField] Transform ejection_point;
    [SerializeField] GameObject particle_effect;
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bullet_casing_prefab;
    [SerializeField] GameObject bullet_prefab;

    private Vector3 target_pos;


    void Update()
    {
        UpdateTargetPos();

        if (can_shoot && Input.GetButtonDown("Fire1"))
            ProcessShot();
    }


    void UpdateTargetPos()
    {
        target_pos = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);
    }


    void ProcessShot()
    {
        List<RaycastHit2D> hits = EvaluateValidGunHits(gun.transform.right, Color.green);
        bool hit_body_exists = hits.Any(elem => elem.rigidbody != null);

        if (hit_body_exists)
        {
            RaycastHit2D hit = hits.Find(elem => elem.rigidbody != null);
            SpawnBullet(hit); // BulletCamShot.
        }
        else
        {
            if (!AimAssistedShot())
                InstantShot(hits);
        }

        SpawnParticle();
        SpawnBulletCasing();

        AudioManager.PlayOneShot("gun_shot");
    }


    // Returns true if the AimAssistedShot spawned a bullet, otherwise returns false.
    bool AimAssistedShot()
    {
        var circle_cast = Physics2D.CircleCast(muzzle.position, scan_tolerance,
            gun.transform.right, scan_distance, cast_hit_layers);

        if (circle_cast.rigidbody == null ||
            Physics2D.Raycast(muzzle.position, gun.transform.right, circle_cast.distance, ray_obstacle_layers))
        {
            return false;
        }

        Debug.DrawLine(muzzle.position, muzzle.position + (gun.transform.right * scan_distance), Color.yellow, 3);
        Debug.Log(circle_cast.collider);

        SpawnBullet(circle_cast);

        return true;
    }


    void InstantShot(List<RaycastHit2D> _hits)
    {
        AddRecoil();

        foreach (RaycastHit2D hit in _hits)
        {
            Scuffable scuffable = hit.collider.GetComponent<Scuffable>();

            if (scuffable != null)
                scuffable.Scuff(new BulletImpact(hit.point, gun.transform.right));
        }
    }


    void AddRecoil()
    {
        gun.AddForce(-gun.transform.right * recoil_force);
    }


    void SpawnParticle()
    {
        var particle = Instantiate(particle_effect, muzzle.position,
            Quaternion.LookRotation(gun.transform.right));
    }


    void SpawnBullet(RaycastHit2D _hit)
    {
        Debug.Log(_hit.rigidbody.name);
        StartCoroutine(BulletCamSequence(_hit));
    }


    void SpawnBulletCasing()
    {
        var casing_clone = Instantiate(bullet_casing_prefab, ejection_point.position, gun.transform.rotation);
        Rigidbody2D casing_body = casing_clone.GetComponent<Rigidbody2D>();

        casing_body.AddForce(gun.transform.right * ejection_force);
        casing_body.AddTorque(Random.Range(min_ejection_torque, max_ejection_torque));
    }


    RaycastHit2D EvaluateGunHit(Vector3 _dir, Color _color)
    {
        RaycastHit2D hit = Physics2D.Raycast(muzzle.position, _dir, scan_distance, ray_hit_layers);
        Debug.DrawLine(muzzle.position, muzzle.position + (gun.transform.right * scan_distance), _color, 3);

        return hit;
    }


    List<RaycastHit2D> EvaluateValidGunHits(Vector3 _dir, Color _color)
    {
        List<RaycastHit2D> valid_hits = new List<RaycastHit2D>();

        RaycastHit2D[] hits = Physics2D.RaycastAll(muzzle.position, _dir, scan_distance, ray_hit_layers);
        Debug.DrawLine(muzzle.position, muzzle.position + (gun.transform.right * scan_distance), _color, 3);

        foreach (RaycastHit2D hit in hits)
        {
            Vector3 target = hit.point;
            Vector3 dir = (target - muzzle.position).normalized;

            if (Physics2D.Raycast(muzzle.position, dir, hit.distance, ray_obstacle_layers))
                continue;

            // Direct path to hit exists.
            valid_hits.Add(hit);
        }

        return valid_hits;
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
        GameManager.scene.player.bod.AddForce(-gun_force);
    }

}
