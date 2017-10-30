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
    private List<Scuffable> things_scuffed = new List<Scuffable>();


    public void Init(Vector3 _dir, RaycastHit2D _hit)
    {
        dir = _dir.normalized;
        expected_hit = _hit;
    }


    void Start()
    {
        GeneralCanvas.distance_text.gameObject.SetActive(true);
    }


    void Update()
    {
        Vector3 prev_pos = transform.position;

        dir = ((Vector3)expected_hit.rigidbody.position - transform.position).normalized;
        transform.position += dir * speed * Time.unscaledDeltaTime;
        transform.rotation = Quaternion.LookRotation(dir);

        float dist_to_target = Vector3.Distance(transform.position, expected_hit.rigidbody.position);
        GeneralCanvas.distance_text.text = (dist_to_target / 5).ToString("F2") + "m";

        Vector3 current_pos = transform.position;

        ScuffCheck(prev_pos, current_pos);

        if (dist_to_target < 0.5f)
        {
            TargetableBody targetable = expected_hit.rigidbody.GetComponent<TargetableBody>();
            if (targetable != null)
                targetable.Hit(new BulletImpact(transform.position, dir, expected_hit.rigidbody));

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

        scuffable.Scuff(new BulletImpact(hit.point, diff, expected_hit.rigidbody));
        things_scuffed.Add(scuffable);
    }


    void OnDestroy()
    {
        GeneralCanvas.distance_text.gameObject.SetActive(false);
    }

}
