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


    public void Init(Vector3 _dir, float _speed, float _force)
    {
        dir = _dir;
        speed = _speed;
    }


    void Start()
    {

    }


    void Update()
    {
        Vector3 prev_pos = transform.position;
        transform.position += dir * speed * Time.unscaledDeltaTime;
        Vector3 current_pos = transform.position;

        Vector3 diff = (current_pos - prev_pos);
        RaycastHit2D hit = Physics2D.Raycast(prev_pos, diff.normalized, diff.magnitude, hit_layers);

        if (hit.rigidbody == null)
            return;

        hit.rigidbody.AddForce(dir * force);
        Destroy(this.gameObject);
    }

}
