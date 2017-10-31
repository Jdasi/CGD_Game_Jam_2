using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{

    [SerializeField]
    float hoverForce = 600.0f;
    [SerializeField]
    float hoverHeight = 8.0f;
    [SerializeField] GameObject head = null;
    [SerializeField] bool is_alive = true;

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
                        //if (hit.transform.tag != "Ragdoll")
                        if (hit.collider.gameObject.layer == 0)
                        {
                            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
                            Vector2 appliedHoverForce = Vector2.up * proportionalHeight * hoverForce;
                            head.GetComponent<Rigidbody2D>().AddForce(appliedHoverForce, ForceMode2D.Force);

                            //Walk();

                            return;
                        }
                    }
                }
            }
        }
    }

    private void Walk()
    {
        Vector2 rightForce = Vector2.right * 50;
        head.GetComponent<Rigidbody2D>().AddForce(rightForce, ForceMode2D.Force);

        Vector2 upForce = Vector2.up * 50;
        head.GetComponent<Rigidbody2D>().AddForce(upForce, ForceMode2D.Force);
    }

    public bool GetAlive()
    {
        return is_alive;
    }

    public void SetAlive(bool alv)
    {
        is_alive = alv;
    }

    public void IsDead()
    {
        is_alive = false;
    }
}
