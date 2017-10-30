using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralCanvas : MonoBehaviour
{
    public static Text distance_text { get { return instance.distance_text_; } }

    [SerializeField] Text distance_text_;

    private static GeneralCanvas instance;


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
    }

}
