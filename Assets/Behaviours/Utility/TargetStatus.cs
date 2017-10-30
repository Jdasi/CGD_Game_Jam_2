using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetStatus : MonoBehaviour
{
    [SerializeField] UnityEvent trigger_events;

    public void KillTarget()
    {
        trigger_events.Invoke();
    }
}
