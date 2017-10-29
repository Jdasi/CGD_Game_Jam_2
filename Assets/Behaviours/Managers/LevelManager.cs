using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Vector3 sniper_cam_goto;
    [SerializeField] float level_time_limit;

    public float level_timer;
    private bool target_dead;


    void Start()
    {
        // Set Timer
        level_timer = 0;
    }


    void Update()
    {
        CheckTimer();

        if (target_dead)
            Debug.Log("Target Dead");
    }


    void CheckTimer()
    {
        level_timer += Time.deltaTime;
        float prev_timer = level_timer;

        // If the player has Run out of time! GAMEOVER
        if (level_timer >= level_time_limit &&
                prev_timer < level_time_limit)
        {
            // Game Over!
            Debug.Log("MISSION FAILED");
        }
    }


    // These get called by Triggers in the Level
    public void SetCameraPlayer()
    {
        GameManager.scene.camera_manager.SetTarget(GameManager.scene.player.bod.transform);
    }


    public void SetCameraSniper()
    {
        GameManager.scene.camera_manager.SetTarget(sniper_cam_goto);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(sniper_cam_goto, 1);
    }


    public void TargetKilled()
    {
        target_dead = true;
    }
}

