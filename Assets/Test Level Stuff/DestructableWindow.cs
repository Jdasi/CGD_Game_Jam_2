using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWindow : MonoBehaviour
{
    [SerializeField] GameObject broken_window;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Instantiate(broken_window, transform.position, broken_window.transform.rotation);

            Destroy(this.gameObject);
        }
    }
}
