using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject assassination_target;

    public Camera main_cam;
    public Camera sniper_cam;

    public Transform target_spawn_pos;
    public Transform target_speech_pos;
    public GameObject player;
    private GameObject target;

    public float level_timer;
    public float level_time_limit;

    private bool cinematic_played;

    // Use this for initialization
    void Start()
    {
        // Set Timer
        level_timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        level_timer += Time.deltaTime;

        if (level_timer > level_time_limit)
        {
            // Game Over!
            Debug.Log("MISSION FAILED");
        }
    }



    public void PlayerInPosition()
    {
        if (cinematic_played != false)
            return;

        SpawnTarget();
        PlayCinematic();
    }



    private void SpawnTarget()
    {
        target = Instantiate(assassination_target, target_spawn_pos.position, transform.rotation);
        target.GetComponent<TargetAI>().SetTarget(target_speech_pos.position);
    }



    private void PlayCinematic()
    {
        cinematic_played = true;

        sniper_cam.GetComponent<SniperCamera>().SetPosition();

        sniper_cam.gameObject.SetActive(true);
        main_cam.gameObject.SetActive(false);
    }



    public void SetCameraMain()
    {
        main_cam.gameObject.SetActive(true);
        sniper_cam.gameObject.SetActive(false);
    }



    public void SetCameraSniper()
    {
        if (cinematic_played != true)
            return;

        sniper_cam.gameObject.SetActive(true);
        main_cam.gameObject.SetActive(false);
    }
}

