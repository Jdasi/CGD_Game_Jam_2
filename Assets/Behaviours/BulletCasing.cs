using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCasing : MonoBehaviour
{
    [SerializeField] AudioClip[] collision_clips;


    void OnCollisionEnter2D(Collision2D _other)
    {
        AudioManager.PlayOneShot(collision_clips[Random.Range(0, collision_clips.Length)]);
    }

}
