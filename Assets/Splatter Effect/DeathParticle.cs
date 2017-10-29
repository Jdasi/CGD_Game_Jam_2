using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathParticle : MonoBehaviour {

    [SerializeField]
    Transform splat;

    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    Color myColor;


    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }


    void OnParticleCollision(GameObject other)
    { 
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        int i = 0;

        while (i < numCollisionEvents)
        {
            float rand_z = Random.Range(0, 360);

            Transform made = Instantiate(splat, collisionEvents[i].intersection, Quaternion.Euler(0, 0, rand_z)) as Transform;
            made.position = new Vector3(made.position.x, made.position.y, 0);
            i++;
        }   
    }
}
