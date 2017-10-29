using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletImpact
{
    public Vector2 pos;
    public Vector3 dir;


    public BulletImpact(Vector2 _pos, Vector3 _dir)
    {
        pos = _pos;
        dir = _dir;
    }

}
