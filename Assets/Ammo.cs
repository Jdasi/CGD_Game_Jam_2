using System.Collections;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            PlayerStatus player = collision.collider.GetComponentInParent<PlayerStatus>();

            if (player != null)
                player.Damage(25);

            Destroy(gameObject);
        }
    }
}
