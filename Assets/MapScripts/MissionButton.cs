using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionButton : MonoBehaviour {

    public List<GameObject> briefs;
    private Animator cameraZoom;
    public Camera cam;

    // Use this for initialization
    void Start ()
    {
        cameraZoom = cam.gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void BriefMission(GameObject Mission)
    {
        cameraZoom.SetTrigger("Return");
        foreach (GameObject brief in briefs)
        {
            brief.SetActive(false);
        }
        Mission.SetActive(true);
        //cameraZoom.SetBool("Start", true);
        cameraZoom.SetTrigger("StartZoom");
    }
    public void PlayMission(string missionName)
    {
        SceneManager.LoadScene(missionName);
    }
}
