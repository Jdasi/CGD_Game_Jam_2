﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplosiveObject : MonoBehaviour
{
    [SerializeField] float explosion_hit_check_range = 3.0f;

    [SerializeField] LayerMask target_layer;
    [SerializeField] GameObject exploded_vehicle;
    [SerializeField] AudioClip explosion;
    [SerializeField] List<CarMovement> move = new List<CarMovement>();

    public void Explode()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        ExplosionTargetCheck();

        Instantiate(exploded_vehicle, transform.position, transform.rotation);

        AudioManager.PlayOneShot(explosion);

       move.ForEach(m => m.enabled = false);
        //Destroy(transform.parent.gameObject);
    }


    void ExplosionTargetCheck()
    {
        // If we hit multiple parts of Ragdoll,
        // We only need to need to make function call once
        bool target_hit = false;

        Collider2D[] hits;

        hits = Physics2D.OverlapCircleAll(transform.position,
            explosion_hit_check_range, target_layer);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Ragdoll") && target_hit == false)
            {
                hit.GetComponentInParent<TargetStatus>().KillTarget();
                target_hit = true;
            }
        }
    }

    // Using for testing Detection Radius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosion_hit_check_range);
    }
}

