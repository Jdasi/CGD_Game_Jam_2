using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWindow : MonoBehaviour
{
    [SerializeField] GameObject broken_window;
    [SerializeField] AudioClip break_clip;


    public void Break()
    {
        Instantiate(broken_window, transform.position, broken_window.transform.rotation);
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
