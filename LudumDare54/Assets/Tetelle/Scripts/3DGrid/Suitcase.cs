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
        foreach (Point pointGrid in Points)
        {
			Gizmos.color = Color.blue;
            Gizmos.DrawCube(new Vector3(pointGrid.Position.x, pointGrid.Position.y, pointGrid.Position.z), cubeSize);   
        } 
    }

	public Point GetPoint(int index)
	{
		return Points[index];
	}

	public bool ContainsFullPoint(ItemHandler item, Point start)
	{
		List<Vector3> rotatedPoints = new List<Vector3>();
		Quaternion rotationQuater = item.Item.Prefab.transform.rotation;

		foreach(Point positionItem in item.Item.Points)
		{
            rotatedPoints.Add(getPointAfterRotation(positionItem.Position, rotationQuater));
		}

        bool contains = false;

		foreach(Vector3 oneRotatedPointItem in rotatedPoints)
		{
			foreach(Point onePointGrid in Points)
			{
                if ((oneRotatedPointItem.x+start.Position.x) == onePointGrid.Position.x &&  (oneRotatedPointItem.y + start.Position.y) == onePointGrid.Position.y && (oneRotatedPointItem.z + start.Position.z) == onePointGrid.Position.z && onePointGrid.IsFull)
				{
                    contains = true;
					break;
				}
			}
		}
		if (!contains)
		{
            foreach (Vector3 oneRotatedPointItem in rotatedPoints)
            {
                foreach (Point onePointGrid in Points)
                {
                    if ((oneRotatedPointItem.x + start.Position.x) == onePointGrid.Position.x && (oneRotatedPointItem.y + start.Position.y) == onePointGrid.Position.y && (oneRotatedPointItem.z + start.Position.z) == onePointGrid.Position.z)
                    {
                        onePointGrid.IsFull=true;
                    }
                }
            }
        }
		return contains;
	}

	private Vector3 getPointAfterRotation(Vector3 pointItem, Quaternion rotation)
	{
		Vector3 rotatedPoint = rotation * pointItem;
		return rotatedPoint;
	}
}
