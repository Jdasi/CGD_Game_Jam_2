using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloMoManager : MonoBehaviour
{
    public static bool bullet_time
    {
        get { return time_scale == BULLET_TIME_SCALE; }
        set { time_scale = value ? BULLET_TIME_SCALE : 1; }
    }

    public static float time_scale;

    [SerializeField] float lerp_speed = 10;

    private static SloMoManager instance;
    private const float BULLET_TIME_SCALE = 0.01f;
    

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

        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

}
