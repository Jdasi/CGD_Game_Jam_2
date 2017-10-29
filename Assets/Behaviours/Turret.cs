using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;
    public Weapon weapon;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) < 50)
            {
                float angle = Vector3.Angle(transform.up, target.position);
                if (angle > 30 && angle < 120)
                    transform.right = -(target.position - transform.position);

            }
        }

    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, 50);
        Debug.DrawRay(transform.position, -transform.right * 50, Color.green);
        if (hit.collider != null)
        {
            weapon.Shoot();
        }
    }

}
