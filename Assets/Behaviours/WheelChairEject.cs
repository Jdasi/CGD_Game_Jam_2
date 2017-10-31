using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChairEject : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float ejection_force;

    [Header("References")]
    [SerializeField] HingeJoint2D hinge_joint;


	void Start ()
    {
		
	}
	


    void Update ()
    {
        if (hinge_joint != null && Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 mouse_pos = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = (mouse_pos - GameManager.scene.player.bod.transform.position).normalized;

            Destroy(hinge_joint);
            GameManager.scene.player.bod.AddForce(dir * ejection_force, ForceMode2D.Impulse);
        }
    }

}
