using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] LayerMask hit_layers;

    private Vector3 dir;
    private float speed;
    private float force;
    private RaycastHit2D expected_hit;
    private float progress;


    public void Init(Vector3 _dir, float _speed, float _force, RaycastHit2D _hit)
    {
        dir = _dir;
        speed = _speed;
        force = _force;
        expected_hit = _hit;
    }


    void Start()
    {

    }


    void Update()
    {
        progress += speed * Time.unscaledDeltaTime;
        transform.position += dir * speed * Time.unscaledDeltaTime;

        if (progress >= expected_hit.distance)
        {
            expected_hit.rigidbody.AddForce(dir * force);
            Destroy(this.gameObject);
        }
    }

}
