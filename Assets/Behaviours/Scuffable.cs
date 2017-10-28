using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scuffable : MonoBehaviour
{
    [SerializeField] UnityEvent scuff_events;


    public void Scuff()
    {
        scuff_events.Invoke();
    }

}
