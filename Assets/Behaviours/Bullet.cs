using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool trajectory_complete { get; private set; }

    [Header("Parameters")]
    [SerializeField] float speed;
    [SerializeField] float force;
    [SerializeField] LayerMask hit_layers;

    private Vector3 dir;
    private RaycastHit2D expected_hit;
    private float progress;


    public void Init(Vector3 _dir, RaycastHit2D _hit)
    {
        dir = _dir.normalized;
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
            trajectory_complete = true;

            Destroy(this.gameObject);
        }
    }

}
