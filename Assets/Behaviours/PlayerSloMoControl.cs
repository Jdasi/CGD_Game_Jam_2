using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSloMoControl : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float lower_bound;
    [SerializeField] float upper_bound;


    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (SloMoManager.time_scale >= lower_bound)
                SloMoManager.time_scale = lower_bound;
        }


        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (SloMoManager.time_scale == lower_bound)
                SloMoManager.time_scale = upper_bound;
        }
    }

}
