using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
enum Target
{
    GANNON,
    DUCHARME,
    NABEEM
}


public class LevelManager : MonoBehaviour
{
    [SerializeField] UnityEvent on_level_complete;
    [SerializeField] float level_time_limit;
    [SerializeField] float final_killcam_delay = 3.0f;
    [SerializeField] Target level_target = Target.NABEEM;

    public float level_timer;
    public bool level_finished { get; set; }


    void Start()
    {
        GeneralCanvas.GameStart();
        level_finished = false;
        // Set Timer
        level_timer = 0;
        AudioManager.PlayMusic(MusicType.GAME);
    }


    void Update()
    {
        CheckTimer();

        if (!level_finished)
            return;


        if (Input.anyKeyDown)
        {
            GeneralCanvas.GameEnd();
            PlayerStatus.immune = false;
            SceneManager.LoadScene(1);
        }
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


    // Called By the Target
    public void TargetKilled()
    {
        switch (level_target)
            {
                case Target.GANNON:
                    MapInfo.gannonAlive = false;
                    break;
                case Target.DUCHARME:
                    MapInfo.ducharmeAlive = false;
                    break;
                case Target.NABEEM:
                    MapInfo.nabeemAlive = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        PlayerStatus.immune = true;
        StartCoroutine(EndLevel(final_killcam_delay));
    }

    
    IEnumerator EndLevel(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        on_level_complete.Invoke();
    }
}

