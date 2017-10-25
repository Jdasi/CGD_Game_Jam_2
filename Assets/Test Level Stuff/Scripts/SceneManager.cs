using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject assassination_target;

    public Transform target_spawn_pos;
    public Transform target_speech_pos;
    public Transform player_spawn_pos;
    public GameObject player;
    private GameObject target;

    public float level_timer;
    public float level_time_limit;

    private bool play_cinematic;

	// Use this for initialization
	void Start ()
    {
        // Set Timer
        level_timer = 0;
        // Set Player Position
        player.transform.position = player_spawn_pos.transform.position;

        play_cinematic = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        level_timer += Time.deltaTime;

        if(level_timer > level_time_limit)
        {
            // Game Over!
            Debug.Log("MISSION FAILED");
        }
	}



    public void PlayerInPosition()
    {
        PlayCinematic();

        InitialiseTarget();
    }



    private void InitialiseTarget()
    {
        target =  Instantiate(assassination_target, target_spawn_pos.position, transform.rotation);
        target.GetComponent<TargetAI>().SetTarget(target_speech_pos.position);
    }



    void PlayCinematic()
    {
        // STOPTIMER
        Debug.Log("PlayingCinematic...");
    }
}
