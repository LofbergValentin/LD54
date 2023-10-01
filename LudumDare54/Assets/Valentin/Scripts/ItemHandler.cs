using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public Item Item;
    public Point CurrentPoint;

    public Vector3 StartPosition;
    public Quaternion StartRotation;

    private void Start()
    {
        StartPosition = transform.position;
        StartRotation = transform.rotation;
    }
}
