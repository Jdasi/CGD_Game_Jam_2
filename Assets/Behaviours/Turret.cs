using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;
    public Weapon weapon;
    public float range = 30;
    public float min_angle = 30;
    public float max_angle = 120;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if player assigned
        if (target != null)
        {
            Debug.Log(Vector3.Dot(transform.right, target.right));
            if (Vector3.Distance(transform.position, target.position) < range && Vector3.Dot(transform.right, target.right) >0) // check if player is in range
            {
                float angle = Vector3.Angle(transform.up, target.position);
                if (angle > min_angle && angle < max_angle)
                    transform.right = -(target.position - transform.position);

            }
        }

    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, range);
        Debug.DrawRay(transform.position, -transform.right * range, Color.green);
        if (hit.collider != null&& hit.collider.CompareTag("Player"))
        {
            weapon.Shoot();
        }
    }

}
