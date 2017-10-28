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

    private Vector3 hook_point;
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
            hook_point = hit.point;
            hook_attached = true;

            hook_line.positionCount = 2;
            hook_line.SetPosition(1, hit.point);

            AudioManager.PlayOneShot("hook_connect");
        }
    }


    void HandleHookRelease()
    {
        if (!Input.GetButtonUp("Fire2"))
            return;

        hook_point = Vector3.zero;
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

        Vector3 dir = (hook_point - hand_rb.transform.position).normalized;
        hand_rb.AddForce(dir * pull_force);
    }

}
