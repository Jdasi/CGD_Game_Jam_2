using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] GameObject exploded_barrel;
    [SerializeField] AudioClip explosion;


    public void Explode()
    {
        Instantiate(exploded_barrel, transform.position, transform.rotation);

        AudioManager.PlayOneShot(explosion);

        Destroy(this.gameObject);
    }
}
