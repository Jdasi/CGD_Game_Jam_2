using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAiming : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float aim_force = 10;
    [SerializeField] float recoil_force;
    [SerializeField] float ejection_force;
    [SerializeField] float hit_force;
    [SerializeField] LayerMask hit_layers;

    [Header("References")]
    [SerializeField] Rigidbody2D gun_aimer;
    [SerializeField] Rigidbody2D gun;
    [SerializeField] Transform ejection_point;
    [SerializeField] GameObject particle_effect;
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bullet_casing_prefab;

    private Vector2 target_pos = new Vector2();
    private bool fire = false;


    void Update()
    {
        UpdateTargetPos();
        fire = Input.GetButtonDown("Fire1");

        if (fire)
        {
            // Recoil.
            gun.AddForce(gun.transform.up * recoil_force);

            var particle = Instantiate(particle_effect);
            particle.transform.position = muzzle.position;
            particle.transform.rotation = muzzle.rotation;

            AudioManager.PlayOneShot("gun_shot");

            var casing_clone = Instantiate(bullet_casing_prefab, ejection_point.position, gun.transform.rotation);
            casing_clone.GetComponent<Rigidbody2D>().AddForce(gun.transform.right * ejection_force);

            Destroy(particle, 5);
            HitScan();
        }
    }


    void HitScan()
    {
        RaycastHit2D hit = Physics2D.Raycast(muzzle.position, -gun.transform.up, Mathf.Infinity, hit_layers);
        Debug.DrawLine(muzzle.position, muzzle.position + (-gun.transform.up * 1000), Color.green, 3);

        if (hit.rigidbody == null)
            return;

        Debug.Log(hit.rigidbody.name);
        Vector3 dir = (hit.transform.position - gun.transform.position).normalized;
        hit.rigidbody.AddForce(dir * hit_force);
    }


	void FixedUpdate ()
    {
		gun_aimer.AddForce(-target_pos.normalized * aim_force);

        
    }


    void UpdateTargetPos()
    {
       Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        target_pos = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f) - mousePos;
    }

}
