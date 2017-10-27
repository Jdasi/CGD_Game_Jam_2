using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperCamera : MonoBehaviour
{
    public Transform sniper_spot;
    public Transform target_position;

    private float pan_speed = 5.0f;
    private bool in_position;

    // Use this for initialization
    void Start()
    {
        in_position = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (in_position == false)
        {
            MoveToTarget();
        }

        if (in_position == true)
        {
            // pan back to player faster though
            pan_speed = 10.0f;
        }
    }



    private void MoveToTarget()
    {
        Vector3 target_pos = new Vector3(target_position.position.x, target_position.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target_pos, Time.deltaTime * pan_speed);
    }



    public void SetPosition()
    {
        transform.position = new Vector3(sniper_spot.transform.position.x, sniper_spot.transform.position.y, transform.position.z);
    }
}
