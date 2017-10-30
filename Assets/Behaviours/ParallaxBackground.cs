using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] float smoothing = 1;

    private Transform rel;
    private Vector3 prev_pos;
    private float parallax_scale;


    void Start()
    {
        rel = Camera.main.transform;
        prev_pos = rel.position;

        parallax_scale = transform.position.z * -1;
    }


    void Update()
    {
        float parallax = (prev_pos.x - rel.transform.position.x) * parallax_scale;
        float target_x = transform.position.x + parallax;

        Vector3 target_pos = new Vector3(target_x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target_pos, smoothing * Time.deltaTime);

        prev_pos = rel.position;
    }

}
