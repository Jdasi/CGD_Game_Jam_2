using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlasher : MonoBehaviour
{
    [SerializeField] List<Graphic> target_elements = new List<Graphic>();
    [SerializeField] float flash_interval = 0.5f;

    private CountdownTimer flash_timer = new CountdownTimer();


	void Start ()
    {
		flash_timer.InitCountDownTimer(flash_interval);
	}
	

	void Update ()
	{
		if (flash_timer.UpdateTimer())
            ToggleElements();

	    flash_timer.timer_duration = flash_interval;
    }


    private void ToggleElements()
    {
        foreach (var ui_element in target_elements)
        {
            ui_element.enabled = !ui_element.enabled;
        }
    }

}
