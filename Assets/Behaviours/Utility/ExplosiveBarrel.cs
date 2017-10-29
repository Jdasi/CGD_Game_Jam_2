using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] UnityEvent target_killed;
    [SerializeField] float explosion_hit_check_range = 3.0f;

    [SerializeField] LayerMask target_layer;
    [SerializeField] GameObject exploded_barrel;
    [SerializeField] AudioClip explosion;

    public void Explode()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        ExplosionTargetCheck();

        Instantiate(exploded_barrel, transform.position, transform.rotation);

        AudioManager.PlayOneShot(explosion);

        Destroy(this.gameObject);
    }


    void ExplosionTargetCheck()
    {
        bool target_hit = false;

        Collider2D[] hits;

        hits = Physics2D.OverlapCircleAll(transform.position,
            explosion_hit_check_range, target_layer);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Ragdoll") && target_hit == false)
            {
                target_killed.Invoke();
                target_hit = true;
            }

            else if (hit.CompareTag("Barrel") && hit.gameObject != this.gameObject)
            {
                hit.GetComponent<ExplosiveBarrel>().Explode();
            }
        }
    }

    // Using for testing Detection Radius
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosion_hit_check_range);
    }*/
}
