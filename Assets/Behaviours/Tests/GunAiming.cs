using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAiming : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float aim_force = 10;
    [SerializeField] float recoil_force;
    [SerializeField] float hit_force;

    [Space]
    [SerializeField] float ejection_force;
    [SerializeField] float min_ejection_torque;
    [SerializeField] float max_ejection_torque;

    [SerializeField] LayerMask hit_layers;
    [SerializeField] WheelchairControl wheelchair;

    [Header("References")]
    [SerializeField] Rigidbody2D gun_aimer;
    [SerializeField] Rigidbody2D gun;
    [SerializeField] Transform ejection_point;
    [SerializeField] GameObject particle_effect;
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bullet_casing_prefab;

    private Vector3 target_pos;
    private bool fire = false;


    void Update()
    {
        UpdateTargetPos();
        fire = Input.GetButtonDown("Fire1");

        if (fire)
        {
            // Recoil.
            gun.AddForce(-gun.transform.right * recoil_force);
            AudioManager.PlayOneShot("gun_shot");

            var particle = Instantiate(particle_effect);
            particle.transform.position = muzzle.position;
            particle.transform.rotation = muzzle.rotation;
            Destroy(particle, 5);

            SpawnBulletCasing();
            HitScan();
        }
    }


    void SpawnBulletCasing()
    {
        var casing_clone = Instantiate(bullet_casing_prefab, ejection_point.position, gun.transform.rotation);
        Rigidbody2D casing_body = casing_clone.GetComponent<Rigidbody2D>();

        casing_body.AddForce(gun.transform.right * ejection_force);
        casing_body.AddTorque(Random.Range(min_ejection_torque, max_ejection_torque));
    }


    void HitScan()
    {
        RaycastHit2D hit = Physics2D.Raycast(muzzle.position, gun.transform.right, Mathf.Infinity, hit_layers);
        Debug.DrawLine(muzzle.position, muzzle.position + (gun.transform.right * 1000), Color.green, 3);

        if (hit.rigidbody == null)
            return;

        Debug.Log(hit.rigidbody.name);
        Vector3 dir = (hit.transform.position - gun.transform.position).normalized;
        hit.rigidbody.AddForce(dir * hit_force);
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


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(target_pos, 1);
    }

}
