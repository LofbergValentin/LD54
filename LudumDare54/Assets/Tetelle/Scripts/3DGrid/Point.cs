using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Point
{
    public Vector3 Position;
    public bool IsFull;
    public Point(Vector3 position){
        this.Position = position;
        this.IsFull = false;
    }
}
