﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject assassination_target;

    public Transform target_spawn_pos;
    public Transform target_speech_pos;
    public GameObject player;
    private GameObject target;

    public float level_timer;
    public float level_time_limit;

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
        PlayCinematic();

        SpawnTarget();
    }



    private void SpawnTarget()
    {
        target = Instantiate(assassination_target, target_spawn_pos.position, transform.rotation);
        target.GetComponent<TargetAI>().SetTarget(target_speech_pos.position);
    }



    void PlayCinematic()
    {
        // STOPTIMER
        Debug.Log("PlayingCinematic...");
    }
}

