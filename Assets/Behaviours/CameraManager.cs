using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraUpdateMode
{
    DELTA,
    FIXED_DELTA
}

public class CameraManager : MonoBehaviour
{
    [HideInInspector] public float original_zoom;

    [Header("Parameters")]
    public Vector3 offset;
    public CameraUpdateMode update_mode;
    [SerializeField] Transform target;
    [SerializeField] float lerp_speed;
    [SerializeField] float zoom_speed;

    [Header("References")]
    [SerializeField] Camera cam;

    private Vector3 target_pos;
    private float target_zoom;


    public void SetTarget(Transform _target, float _zoom)
    {
        target = _target;
        target_pos = Vector3.zero;
        target_zoom = _zoom;
    }


    public void SetTarget(Vector3 _target, float _zoom)
    {
        target_pos = _target;
        target = null;
        target_zoom = _zoom;
    }


    public void SetTarget(Transform _target)
    {
        SetTarget(_target, target_zoom);
    }


    public void SetTarget(Vector3 _target)
    {
        SetTarget(_target, target_zoom);
    }


    void Start()
    {
        target_zoom = cam.orthographicSize;
        original_zoom = target_zoom;
    }


    void Update()
    {
        target_zoom = Mathf.Clamp(target_zoom, 0, 100);
        UpdateZoom();

        if (update_mode == CameraUpdateMode.DELTA)
            UpdatePosition();
    }

    
    void FixedUpdate()
    {
        if (update_mode == CameraUpdateMode.FIXED_DELTA)
            UpdatePosition();
    }


    void UpdatePosition()
    {
        target_pos = (target != null ? target.position : target_pos) + offset;

        transform.position = Vector3.Lerp(transform.position, target_pos,
            lerp_speed * GetCurrentDelta());
    }


    void UpdateZoom()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,
            target_zoom, zoom_speed * GetCurrentDelta());
    }


    float GetCurrentDelta()
    {
        return update_mode == CameraUpdateMode.DELTA ?
            Time.unscaledDeltaTime : Time.fixedUnscaledDeltaTime;
    }

}
