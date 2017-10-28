using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scuffable : MonoBehaviour
{
    [SerializeField] CustomEvents.Vector3Event scuff_events;


    public void Scuff(Vector3 _scuff_point)
    {
        scuff_events.Invoke(_scuff_point);
    }

}
