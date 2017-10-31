using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField] Rigidbody2D title;
    [SerializeField] Vector2 jump_force;
    [SerializeField] Collider2D stopper;
    [SerializeField] int scene_build_index;
    [SerializeField] float load_delay = 4;
    [SerializeField] GameObject start_title;

    private bool loading = false;


    private void Update()
    {
        if (!Input.GetButtonDown("Submit") || loading)
            return;

        start_title.SetActive((false));
        loading = true;
        StartGame();
    }


    private void StartGame()
    {
        AnimateTitle();
        Invoke("LoadNextScene", load_delay);
    }


    private void AnimateTitle()
    {
        title.AddForce(jump_force);
        stopper.enabled = false;
    }


    private void LoadNextScene()
    {
        SceneManager.LoadScene(scene_build_index);
    }

}
