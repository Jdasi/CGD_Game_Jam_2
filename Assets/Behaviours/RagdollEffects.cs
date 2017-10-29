using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEffects : MonoBehaviour
{
    [SerializeField] float bullet_hit_force;
    [SerializeField] GameObject blood_splat;


    public void HitHead(BulletImpact _impact)
    {
        AddForce(_impact);
        SpawnSplat(_impact.body.transform.position);
    }


    public void HitBody(BulletImpact _impact)
    {
        AddForce(_impact);
        SpawnSplat(_impact.body.transform.position);
    }


    public void HitLimb(BulletImpact _impact)
    {
        AddForce(_impact);
        SpawnSplat(_impact.body.transform.position);
    }


    void SpawnSplat(Vector3 _pos)
    {
        Instantiate(blood_splat, _pos, Quaternion.identity);
    }


    void AddForce(BulletImpact _impact)
    {
        _impact.body.AddForce(_impact.dir * bullet_hit_force);
    }

}
