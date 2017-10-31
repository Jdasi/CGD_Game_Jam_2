using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathParticle : MonoBehaviour {

    [SerializeField] Transform splat;
    [SerializeField] float min_size;
    [SerializeField] float max_size;

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

        foreach (ParticleCollisionEvent p_event in collisionEvents)
        {
            float rand_z = Random.Range(0, 360);
            float rand_size = Random.Range(min_size, max_size);

            Transform clone = Instantiate(splat, p_event.intersection, Quaternion.Euler(0, 0, rand_z)) as Transform;
            clone.position = new Vector3(clone.position.x, clone.position.y, 0);
            clone.localScale *= rand_size;
        }   
    }
}
