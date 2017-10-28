using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetableBody : MonoBehaviour
{
    [SerializeField] UnityEvent hit_events;


    public void Hit()
    {
        hit_events.Invoke();
    }

}
