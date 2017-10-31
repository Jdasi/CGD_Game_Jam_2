using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MissionButton : MonoBehaviour
{

    public List<GameObject> briefs;
    private bool alive;
    private int fadeSpeed;

    public GameObject CameraScript;
    private List<CanvasGroup> panels = new List<CanvasGroup>();
    private CanvasGroup current_panel;

    // Use this for initialization
    void Start()
    {
        alive = true;
        fadeSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (CanvasGroup panel in panels)
        {
            if (current_panel == panel)
            {
                if (panel.alpha < 1)
                    panel.alpha += fadeSpeed * Time.deltaTime;
            }
            else
            {
                if (panel.alpha > 0)
                    panel.alpha -= fadeSpeed * Time.deltaTime;
            }
        }
    }

    public void ShowGui(CanvasGroup Panel)
    {
        if (!panels.Contains(Panel))
            panels.Add(Panel);

        current_panel = Panel;
    }

    public void HideGui(CanvasGroup Panel)
    {
        current_panel = null;
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
        DontDestroyOnLoad(GameObject.Find("MapScript"));
    }
}
