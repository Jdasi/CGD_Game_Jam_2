using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosion : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    public void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        AudioManager.PlayOneShot("Explosion");
    }
}
