using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererSort : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    void Start()
    {
        LineRenderer line = GetComponent<LineRenderer>();

        line.sortingLayerID = sprite.sortingLayerID;
        line.sortingOrder = sprite.sortingOrder;
    }
}
