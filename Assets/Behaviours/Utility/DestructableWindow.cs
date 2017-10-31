using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWindow : MonoBehaviour
{
    [SerializeField] GameObject[] broken_windows;
    [SerializeField] GameObject force_explosion;
    [SerializeField] AudioClip break_clip;

    private bool fired;


    public void Break(bool _with_sound = true)
    {
        fired = true;

        GameObject prefab = broken_windows[Random.Range(0, broken_windows.Length)];
        Instantiate(prefab, transform.position, prefab.transform.rotation);

        if (_with_sound)
            AudioManager.PlayOneShot(break_clip);

        Destroy(this.gameObject);
    }


    public void BreakShot(BulletImpact _impact)
    {
        Instantiate(force_explosion, _impact.pos, force_explosion.transform.rotation);
        bool unscaled_sound = _impact.body != null;
        
        if (unscaled_sound)
            AudioManager.PlayOneShotUnscaled(break_clip);

        Break(!unscaled_sound);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ragdoll") || other.CompareTag("Debris"))
        {
            if (!fired)
                Break();
        }
    }

}
