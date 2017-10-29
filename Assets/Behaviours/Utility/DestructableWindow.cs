using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWindow : MonoBehaviour
{
    [SerializeField] GameObject[] broken_windows;
    [SerializeField] GameObject force_explosion;
    [SerializeField] AudioClip break_clip;

    private bool fired;


    public void Break()
    {
        fired = true;

        GameObject prefab = broken_windows[Random.Range(0, broken_windows.Length)];
        Instantiate(prefab, transform.position, prefab.transform.rotation);

        AudioManager.PlayOneShot(break_clip);

        Destroy(this.gameObject);
    }


    public void BreakShot(BulletImpact _impact)
    {
        Instantiate(force_explosion, _impact.pos, force_explosion.transform.rotation);

        Break();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ragdoll"))
        {
            if (!fired)
                Break();
        }
    }

}
