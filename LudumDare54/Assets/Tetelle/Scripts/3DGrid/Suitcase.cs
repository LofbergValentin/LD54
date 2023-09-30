using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Suitcase : MonoBehaviour
{
    [SerializeField] private int height;
	[SerializeField] private int width;
	[SerializeField] private int length;

	[SerializeField] private Vector3 cubeSize;

	[SerializeField] private List<Point> points;

    public List<Point> Points { get => points; set => points = value; }

    public Suitcase(int height, int width, int length)
	{
		this.height = height;
		this.width = width;
		this.length = length;
	}
	private void Awake()
	{
        Points = new List<Point>();
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				for (int k = 0; k < length; k++)
				{
					this.Points.Add(new Point(transform.position + new Vector3(i,j,k)));
				}
			}
		}
	}
    private void OnDrawGizmos() 
    {
        foreach (Point point in Points)
        {
			Gizmos.color = Color.blue;
            Gizmos.DrawCube(new Vector3(point.Position.x, point.Position.y,point.Position.z), cubeSize);   
        } 
    }

	public Point GetPoint(int index)
	{
		return Points[index];
	}

	public bool ContainsFullPoint(List<Vector3> vector3s, Point start)
	{
        bool contains = false;
		foreach(Vector3 vector3 in vector3s)
		{
			foreach(Point point in Points)
			{
                if ((vector3.x+start.Position.x) == point.Position.x &&  (vector3.y + start.Position.y) == point.Position.y && (vector3.z + start.Position.z) == point.Position.z && point.IsFull)
				{
                    contains = true;
					break;
				}
			}
		}
		if (!contains)
		{
            foreach (Vector3 vector3 in vector3s)
            {
                foreach (Point point in Points)
                {
                    if ((vector3.x + start.Position.x) == point.Position.x && (vector3.y + start.Position.y) == point.Position.y && (vector3.z + start.Position.z) == point.Position.z)
                    {
                        point.IsFull=true;
                    }
                }
            }
        }
		return contains;
	}
}
