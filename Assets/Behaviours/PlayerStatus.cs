using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    public bool alive { get { return current_health > 0; } }

    [SerializeField] int starting_health = 100;
    [SerializeField] UnityEvent death_events;
    [SerializeField] Color hurt_color;
    [SerializeField] float hurt_fade_time;

    private int current_health;


    public void Kill()
    {
        current_health = 0;
        death_events.Invoke();
    }

    
    public void Damage(int _amount)
    {
        if (!alive)
            return;

        current_health -= _amount;
        GeneralCanvas.health_fill.fillAmount -= (float)_amount / 100;
        GeneralCanvas.damage_fade.FadeColor(hurt_color, Color.clear, hurt_fade_time);
        AudioManager.PlayOneShot("grunt");

        if (current_health <= 0)
            Kill();
    }


    void Start()
    {
        current_health = starting_health;
    }
    
}
