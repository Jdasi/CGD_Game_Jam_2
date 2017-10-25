using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAiming : MonoBehaviour
{
    [SerializeField] Rigidbody2D gun_aimer;
    [SerializeField] Rigidbody2D gun;
    [SerializeField] private GameObject particle_effect;
    [SerializeField] private Transform muzzle;
    [SerializeField] float aim_force = 10;

    private Vector2 target_pos = new Vector2();
    private bool fire = false;


    void Update()
    {
        UpdateTargetPos();
        fire = Input.GetButtonDown("Fire1");
        if (fire)
        {
            gun.AddForce(gun.transform.up * 10000);
            var particle = Instantiate(particle_effect);
            particle.transform.position = muzzle.position;
            particle.transform.rotation = muzzle.rotation;
            Destroy(particle, 5);
        }
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
