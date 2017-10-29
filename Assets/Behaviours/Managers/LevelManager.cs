using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Vector3 sniper_cam_goto;
    [SerializeField] float level_time_limit;

    public float level_timer;


    void Start()
    {
        // Set Timer
        level_timer = 0;
    }


    void Update()
    {
        float prev_timer = level_timer;
        level_timer += Time.deltaTime;

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

}

