using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private Animator cameraZoom;
    public Camera cam;
    // Use this for initialization
    void Start ()
    {
        cameraZoom = cam.gameObject.GetComponent<Animator>();
        cameraZoom.SetTrigger("StartZoom");
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void MissionOne()
    {
        cameraZoom.SetTrigger("Mission1Enter");
    }
    public void MissionTwo()
    {
        cameraZoom.SetTrigger("Mission1Enter");
    }
    public void MissionThree()
    {
        cameraZoom.SetTrigger("Mission1Enter");
    }
    public void ReturnToMap()
    {
        cameraZoom.SetTrigger("ReturnToMap");
        cameraZoom.ResetTrigger("Mission1Enter");
    }
}
