using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Drawing;

[Serializable]
public class Point
{
    public Vector3 Position;
    public bool IsFull;
    public Point(Vector3 position){
        this.Position = position;
        this.IsFull = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(new Vector3(Position.x, Position.y, Position.z), new Vector3(0.95f, 0.95f, 0.95f));
    }
}
