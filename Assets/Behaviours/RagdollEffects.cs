using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEffects : MonoBehaviour
{
    [SerializeField]
    float bullet_hit_force;
    [SerializeField]
    GameObject blood_splat;
    [SerializeField]
    Hover hover_script;

    void Start()
    {
        hover_script = GetComponent<Hover>();
    }

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
        AudioManager.PlayOneShot("blood_hit");
        Instantiate(blood_splat, _pos, Quaternion.identity);
    }


    void AddForce(BulletImpact _impact)
    {
        if (hover_script.GetAlive())
        {
            hover_script.SetAlive(false);
        }

        _impact.body.AddForce(_impact.dir * bullet_hit_force, ForceMode2D.Impulse);
    }

}
