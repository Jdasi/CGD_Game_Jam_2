using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public enum PlayerState
{
    WAITING,
    JOINING,
    PLAYING,
    LEAVING
}

public class ConnectedPlayer
{
    public Player input;
    public int id { get { return input.id; } }
    public PlayerState state = PlayerState.WAITING;
    public GameObject character;
    public Color color;

    private const float TIME_TO_IDLE = 20;
    private float idle_timer;

    private float horizontal;
    private float vertical;


    public void Update()
    {
        HandleDropIn();
        HandleIdle();

        ControlCharacter();
    }


    void HandleDropIn()
    {
		if (input.GetButtonDown("DropIn"))
        {
            if (state == PlayerState.WAITING)
            {
                state = PlayerState.JOINING;
                idle_timer = 0;
            }
            else if (state == PlayerState.PLAYING)
            {
                state = PlayerState.LEAVING;
                idle_timer = 0;
            }
        }
    }


    void HandleIdle()
    {
        if (state != PlayerState.PLAYING)
            return;

        idle_timer += Time.unscaledDeltaTime;

        if (idle_timer >= TIME_TO_IDLE)
        {
            state = PlayerState.LEAVING;
            idle_timer = 0;
        }
    }


    void ControlCharacter()
    {
        horizontal = input.GetAxis("Horizontal");
        vertical = input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0 || input.GetAnyButton())
            idle_timer = 0;

        CharacterControl();
    }


    void CharacterControl()
    {
        if (input.GetButtonDown("B"))
            WakePlayer();
    }


    void WakePlayer()
    {
        if (state != PlayerState.WAITING)
            return;

        state = PlayerState.JOINING;
        idle_timer = 0;
    }

}
