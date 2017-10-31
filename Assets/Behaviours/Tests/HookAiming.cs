using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookAiming : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float grapple_distance;
    [SerializeField] float pull_force;
    [SerializeField] LayerMask hit_layers;
    [SerializeField] LineRenderer hook_line;

    [Header("References")]
    [SerializeField] Rigidbody2D hand_rb;

    private Transform hook_point;
    private bool hook_attached;


    void Update()
    {
        HandleHookFire();
        HandleHookRelease();
        
        UpdateHook();
    }


    void HandleHookFire()
    {
        if (SloMoManager.bullet_time || !Input.GetButtonDown("Fire2"))
            return;

        Vector3 mouse_pos = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(hand_rb.transform.position, mouse_pos - hand_rb.transform.position,
            grapple_distance, hit_layers);

        if (hit.collider != null)
        {
            hook_point = new GameObject().transform;
            hook_point.name = "Hook Point";

            hook_point.position = hit.point;
            hook_point.SetParent(hit.collider.transform);

            hook_attached = true;
            hook_line.positionCount = 2;

            AudioManager.PlayOneShot("hook_connect");
        }
    }


    void HandleHookRelease()
    {
        if (!hook_attached || !Input.GetButtonUp("Fire2"))
            return;

        Destroy(hook_point.gameObject);
        hook_attached = false;

        hook_line.positionCount = 0;

        AudioManager.PlayOneShot("hook_release");
    }


    void UpdateHook()
    {
        if (hook_attached)
        {
            hook_line.enabled = true;
            hook_line.SetPosition(0, hand_rb.transform.position);
            hook_line.SetPosition(1, hook_point.position);
        }
        else
        {
            hook_line.enabled = false;
        }
    }


    void FixedUpdate()
    {
        if (!hook_attached)
            return;

        Vector3 dir = (hook_point.position - hand_rb.transform.position).normalized;
        hand_rb.AddForce(dir * pull_force);
    }

}
