using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float bullet_speed;
    [SerializeField] float bullet_force;
    [SerializeField] float destroy_delay;
    [SerializeField] LayerMask hit_layers;
    
    private Vector3 dir;


    public void Init(Vector3 _dir)
    {
        dir = _dir;
    }


    void Start()
    {
        Invoke("KillBullet", destroy_delay);
    }


    void Update()
    {
        Vector3 prev_pos = transform.position;
        transform.position += dir * bullet_speed * Time.deltaTime;
        Vector3 current_pos = transform.position;

        HitCheck(prev_pos, current_pos);
    }


    void HitCheck(Vector3 _prev_pos, Vector3 _current_pos)
    {
        Vector3 diff = (_prev_pos - _current_pos);
        RaycastHit2D hit = Physics2D.Raycast(_prev_pos, diff.normalized,
            diff.magnitude, hit_layers);

        if (!hit)
            return;

        if (hit.collider.CompareTag("Player"))
        {
            hit.rigidbody.AddForce(dir * bullet_force, ForceMode2D.Impulse);
        }

        Destroy(this.gameObject);
    }


    void KillBullet()
    {
        Destroy(this.gameObject);
    }

}
