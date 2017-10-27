using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionButton : MonoBehaviour {

    public List<GameObject> briefs;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void StartMission(int missionID)
    {
        
    }

    public void BriefMission(GameObject Mission)
    {
        foreach (GameObject brief in briefs)
        {
            brief.SetActive(false);
        }
        Mission.SetActive(true);
    }
}
