using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallReveal : MonoBehaviour {

    [SerializeField] GameObject Outside_wall;
    private Renderer[] walls;


	// Use this for initialization
	void Start ()
    {
        walls = Outside_wall.GetComponentsInChildren<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player") 
        {
            //Outside_wall.SetActive(false);

            foreach (Renderer wall in walls)
            {
                Color Aph = wall.material.color;

                Aph.a = 0.1f;

                wall.material.color = Aph;
            }
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            //Outside_wall.SetActive(true);

            foreach (Renderer wall in walls)
            {
                Color Aph = wall.material.color;

                Aph.a = 1;

                wall.material.color = Aph;
            }
        }
    }
}
