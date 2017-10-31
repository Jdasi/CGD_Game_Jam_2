using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float aim_force;
    [SerializeField] float engage_distance;
    [SerializeField] float engage_angle;
    [SerializeField] float shoot_delay;
    [SerializeField] AudioClip[] shot_clips;

    [Header("References")]
    [SerializeField] Rigidbody2D turret_base;
    [SerializeField] Rigidbody2D turret_barrel;
    [SerializeField] Transform shoot_point;
    [SerializeField] SpriteRenderer status_indicator;
    [SerializeField] GameObject bullet_prefab;
    [SerializeField] GameObject shoot_particle_prefab;
    [SerializeField] LineRenderer laser_line;

    [Header("Effects")]
    [SerializeField] Sprite broken_lightbulb;
    [SerializeField] Color scanning_color;
    [SerializeField] Color engaging_color;
    [SerializeField] Color disabled_color;

    private Rigidbody2D player_bod;
    private bool engaging = true;
    private Vector3 dir;
    private float shoot_timer;


    public void Kill()
    {
        this.enabled = false;
        status_indicator.color = disabled_color;
        status_indicator.sprite = broken_lightbulb;

        turret_base.gameObject.layer = 0;
        turret_barrel.gameObject.layer = 0;

        laser_line.enabled = false;
    }

    
    void Start()
    {

    }


    void Update()
    {
        if (player_bod == null)
            player_bod = GameManager.scene.player.bod;

        float player_dist = Vector3.Distance(transform.position, player_bod.transform.position);
        laser_line.enabled = engaging = player_dist <= engage_distance;

        status_indicator.color = engaging ? engaging_color : scanning_color;

        if (shoot_timer > 0)
            shoot_timer -= Time.deltaTime;

        if (engaging)
        {
            HandleEngagement();
        }
    }


    void HandleEngagement()
    {
        dir = (player_bod.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(dir, turret_barrel.transform.up);

        if (angle <= engage_angle)
        {
            HandleShot();
        }
    }


    void HandleShot()
    {
        if (shoot_timer > 0)
            return;

        shoot_timer = shoot_delay;
        AudioManager.PlayOneShot(shot_clips[Random.Range(0, shot_clips.Length)]);

        var particle = Instantiate(shoot_particle_prefab, shoot_point.position,
            Quaternion.LookRotation(turret_barrel.transform.up));

        var bullet_clone = Instantiate(bullet_prefab, shoot_point.position,
            Quaternion.identity);

        EnemyBullet bullet = bullet_clone.GetComponent<EnemyBullet>();
        bullet.Init(dir);
    }


    void FixedUpdate()
    {
        if (!engaging || player_bod == null)
            return;

        turret_barrel.AddForce(dir * aim_force);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, engage_distance);
    }


}
