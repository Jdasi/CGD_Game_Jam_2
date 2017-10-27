using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TempSceneRefs
{
    public PlayerBod player
    {
        get
        {
            if (player_ == null)
                player_ = GameObject.FindObjectOfType<PlayerBod>();

            return player_;
        }
    }


    public CameraManager camera_manager
    {
        get
        {
            if (camera_manager_ == null)
                camera_manager_ = GameObject.FindObjectOfType<CameraManager>();

            return camera_manager_;
        }
    }


    private PlayerBod player_;
    private CameraManager camera_manager_;

}
