using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEffects : MonoBehaviour
{
    [SerializeField] float bullet_hit_force;


    public void HitHead(BulletImpact _impact)
    {
        AddForce(_impact);

    }


    public void HitBody(BulletImpact _impact)
    {
        AddForce(_impact);

    }


    public void HitLimb(BulletImpact _impact)
    {
        AddForce(_impact);

    }


    void AddForce(BulletImpact _impact)
    {
        _impact.body.AddForce(_impact.dir * bullet_hit_force);
    }

}
