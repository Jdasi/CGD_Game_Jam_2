using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWindow : MonoBehaviour
{
    [SerializeField] GameObject broken_window;
    [SerializeField] GameObject force_explosion;
    [SerializeField] AudioClip break_clip;


    public void Break()
    {
        Instantiate(broken_window, transform.position, broken_window.transform.rotation);
        AudioManager.PlayOneShot(break_clip);

        Destroy(this.gameObject);
    }


    public void BreakShot(Vector3 _explosion_point)
    {
        var br_window = Instantiate(broken_window, transform.position, broken_window.transform.rotation);
        var explosion = Instantiate(force_explosion, _explosion_point, force_explosion.transform.rotation);
        //explosion.transform.SetParent(br_window.transform);

        AudioManager.PlayOneShot(break_clip);

        Destroy(this.gameObject);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Break();
        }
    }

}
