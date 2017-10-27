using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Camera main_cam;
    public Camera sniper_cam;

    public GameObject player;
    public GameObject target;

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

        EnableTarget();
        PlayCinematic();
    }



    private void EnableTarget()
    {
        if (target.activeInHierarchy)
            return;
        
        target.gameObject.SetActive(true);
    }



    private void PlayCinematic()
    {
        // Stops PlayerInPosition getting called more than once
        cinematic_played = true;

        sniper_cam.GetComponent<SniperCamera>().SetPosition();

        // Switch Camera for Cinematic...
        sniper_cam.gameObject.SetActive(true);
        main_cam.gameObject.SetActive(false);
    }



    // Switch Cameras...

    // These get called by Triggers in the Level
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

