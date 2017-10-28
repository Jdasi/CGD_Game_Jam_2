using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool trajectory_complete { get; private set; }

    [Header("Parameters")]
    [SerializeField] float speed;
    [SerializeField] float force;
    [SerializeField] LayerMask scuffable_layer;

    private Vector3 dir;
    private RaycastHit2D expected_hit;
    private float progress;
    private List<Scuffable> things_scuffed = new List<Scuffable>();


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
        Vector3 prev_pos = transform.position;

        progress += speed * Time.unscaledDeltaTime;
        transform.position += dir * speed * Time.unscaledDeltaTime;

        Vector3 current_pos = transform.position;

        ScuffCheck(prev_pos, current_pos);

        if (progress >= expected_hit.distance)
        {
            expected_hit.rigidbody.AddForce(dir * force);
            trajectory_complete = true;

            Destroy(this.gameObject);
        }
    }


    void ScuffCheck(Vector3 _prev_pos, Vector3 _current_pos)
    {
        Vector3 diff = (_prev_pos - _current_pos);
        RaycastHit2D hit = Physics2D.Raycast(_prev_pos, diff.normalized,
            diff.magnitude, scuffable_layer);

        if (!hit)
            return;

        Scuffable scuffable = hit.collider.GetComponent<Scuffable>();
        if (scuffable == null || things_scuffed.Contains(scuffable))
            return;

        scuffable.Scuff();
        things_scuffed.Add(scuffable);
    }

}
