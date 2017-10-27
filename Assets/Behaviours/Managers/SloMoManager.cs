using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloMoManager : MonoBehaviour
{
    public static float time_scale;

    [SerializeField] float lerp_speed = 10;

    private static SloMoManager instance;
    

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

        time_scale = Time.timeScale;
    }


    void Update()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, time_scale,
            lerp_speed * Time.deltaTime);
    }

}
