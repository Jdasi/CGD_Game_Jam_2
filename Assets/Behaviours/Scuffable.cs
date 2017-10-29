using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scuffable : MonoBehaviour
{
    [SerializeField] CustomEvents.BulletImpactEvent scuff_events;


    public void Scuff(BulletImpact _impact)
    {
        scuff_events.Invoke(_impact);
    }

}
