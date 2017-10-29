using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetableBody : MonoBehaviour
{
    [SerializeField] CustomEvents.BulletImpactEvent hit_events;


    public void Hit(BulletImpact _impact)
    {
        hit_events.Invoke(_impact);
    }

}
