using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MissionButton : MonoBehaviour {

    public List<GameObject> briefs;
    private bool alive;
    private int fadeSpeed;

    public GameObject CameraScript;

    // Use this for initialization
    void Start ()
    {
        alive = true;
        fadeSpeed = 4;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ShowGui(CanvasGroup Panel)
    {
        StartCoroutine(Show(Panel));
    }

    public void HideGui(CanvasGroup Panel)
    {
        StartCoroutine(Hide(Panel));
    }
    private IEnumerator Show(CanvasGroup panel)
    {
        while(panel.alpha < 1)
        {
            panel.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
    private IEnumerator Hide(CanvasGroup panel)
    {
        while (panel.alpha > 0)
        {
            panel.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    public void BriefMission1(GameObject BriefOne)
    {
        Reset();
        BriefOne.SetActive(true);
        CameraScript.GetComponent<CameraScript>().MissionOne();
    }
    public void BriefMission2(GameObject BriefTwo)
    {
        Reset();
        BriefTwo.SetActive(true);
        CameraScript.GetComponent<CameraScript>().MissionTwo();
    }
    public void BriefMission3(GameObject BriefThree)
    {
        Reset();
        BriefThree.SetActive(true);
        CameraScript.GetComponent<CameraScript>().MissionThree();
    }
    public void GetBackToMap()
    {
        CameraScript.GetComponent<CameraScript>().ReturnToMap();
        Reset();
    }

    private void Reset()
    {
        foreach (GameObject brief in briefs)
        {
            brief.SetActive(false);
        }
    }

    public void PlayMission(string missionName)
    {
        SceneManager.LoadScene(missionName);
    }
}
