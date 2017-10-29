using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWindow : MonoBehaviour
{
    [SerializeField] GameObject broken_window;
    [SerializeField] GameObject force_explosion;
    [SerializeField] AudioClip break_clip;

    private bool fired;


    public void Break()
    {
        fired = true;

        Instantiate(broken_window, transform.position, broken_window.transform.rotation);
        AudioManager.PlayOneShot(break_clip);

        Destroy(this.gameObject);
    }


    public void BreakShot(BulletImpact _impact)
    {
        Instantiate(broken_window, transform.position, broken_window.transform.rotation);
        Instantiate(force_explosion, _impact.pos, force_explosion.transform.rotation);

        AudioManager.PlayOneShot(break_clip);

        Destroy(this.gameObject);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(!fired)
            Break();
        }
    }

}
