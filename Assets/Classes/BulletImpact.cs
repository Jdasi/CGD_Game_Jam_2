using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletImpact
{
    public Vector2 pos;
    public Vector3 dir;
    public Rigidbody2D body;


    public BulletImpact(Vector2 _pos, Vector3 _dir)
    {
        pos = _pos;
        dir = _dir;
    }


    public BulletImpact(Vector2 _pos, Vector3 _dir, Rigidbody2D _body)
    {
        pos = _pos;
        dir = _dir;
        body = _body;
    }

}
