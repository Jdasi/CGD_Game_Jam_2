using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralCanvas : MonoBehaviour
{
    public static Text distance_text { get { return instance.distance_text_; } }
    public static Image health_fill { get { return instance.health_fill_; } }
    public static FadableGraphic damage_fade { get { return instance.damage_fade_; } }

    [SerializeField] Text distance_text_;
    [SerializeField] GameObject health_panel;
    [SerializeField] Image health_fill_;
    [SerializeField] FadableGraphic damage_fade_;

    private static GeneralCanvas instance;


    public static void GameStart()
    {
        distance_text.gameObject.SetActive(false);
        instance.health_panel.gameObject.SetActive(true);
        health_fill.fillAmount = 1;
        damage_fade.FadeOut(0);
    }


    public static void GameEnd()
    {
        distance_text.gameObject.SetActive(false);
        instance.health_panel.gameObject.SetActive(false);
        damage_fade.FadeOut(0);
    }


    void Awake()
    {
        if (instance == null)
        {
            InitSingleton();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void InitSingleton()
    {
        instance = this;
        damage_fade.Init();
    }


    void OnLevelWasLoaded(int level)
    {
        GameEnd();
    }

}
