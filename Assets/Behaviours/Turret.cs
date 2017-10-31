using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION_X
{
    LEFT = -1,
    RIGHT = 1
}

public class Turret : MonoBehaviour
{

    public Transform target;
    public Weapon weapon;
    public float range = 30;
    public float min_angle = 30;
    public float max_angle = 120;
    public DIRECTION_X direction;
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
            if (Vector3.Distance(transform.position, target.position) < range) // check if player is in range
            {
                float angle = Vector3.Angle(transform.up, target.position);
                if (angle > min_angle && angle < max_angle)
                {
                    Vector3 new_right = (int)direction *(target.position - transform.position);
                    transform.right = Vector3.Slerp(transform.right, new_right, Time.deltaTime);
                }

            }
            else
            {
                transform.right = Vector3.Slerp(transform.right, Vector3.right, Time.deltaTime);
            }
        }

    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * (int)direction, range);
        Debug.DrawRay(transform.position, transform.right * range * (int)direction, Color.green);
        if (hit.collider != null&& hit.collider.CompareTag("Player"))
        {
            weapon.Shoot(direction);
        }
    }

}
