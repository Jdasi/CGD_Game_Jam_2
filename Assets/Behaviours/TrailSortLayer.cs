using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailSortLayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    void Start()
    {
        TrailRenderer trail = GetComponent<TrailRenderer>();

        trail.sortingLayerID = sprite.sortingLayerID;
        trail.sortingOrder = sprite.sortingOrder - 1;
    }
}
