﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    private float alive = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        alive += Time.deltaTime;
        if (alive > 2)
        {
            Destroy(gameObject);
        }
    }
}