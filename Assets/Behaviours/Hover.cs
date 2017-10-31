using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{

    [SerializeField]
    float hoverForce = 150.0f;
    [SerializeField]
    float hoverHeight = 10.0f;
    private GameObject head = null;
    private bool is_alive = true;

    // Use this for initialization
    void Start()
    {
        head = transform.Find("head").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (is_alive)
        {
            if (head)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(head.transform.position, -transform.up, hoverHeight);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit)
                    {
                        if (hit.transform.tag != "Ragdoll")
                        {
                            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
                            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
                            head.GetComponent<Rigidbody2D>().AddForce(appliedHoverForce, ForceMode2D.Force);
                        }
                    }
                }
            }
        }
    }

    public bool GetAlive()
    {
        return is_alive;
    }

    public void SetAlive(bool alv)
    {
        is_alive = alv;
    }
}
