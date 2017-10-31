using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] UnityEvent on_level_complete;
    [SerializeField] float level_time_limit;
    [SerializeField] float final_killcam_delay = 3.0f;

    public float level_timer;
    public bool level_finished { get; set; }



    void Start()
    {
        level_finished = false;
        // Set Timer
        level_timer = 0;
    }


    void Update()
    {
        CheckTimer();

        if (!level_finished)
            return;

        if (Input.anyKey)
            SceneManager.LoadScene(1);
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
        level_finished = true;
        StartCoroutine(EndLevel(final_killcam_delay));
    }

    
    IEnumerator EndLevel(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        on_level_complete.Invoke();
    }
}

