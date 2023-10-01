using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Color = UnityEngine.Color;

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
			if(pointGrid.IsFull)
                Gizmos.color = Color.red;
			else
				Gizmos.color = Color.blue;
            Gizmos.DrawCube(new Vector3(pointGrid.Position.x, pointGrid.Position.y, pointGrid.Position.z), cubeSize);   
        } 
    }

	public Point GetPoint(int index)
	{
		return Points[index];
	}

    public void ContainsFullPoint(ItemHandler item, Point start)
    {
        List<Point> rotatedPoints = GenerateRotatedPosition(item);
        if (CheckPointForItem(item, start))
        {
            foreach (Point itemPoint in rotatedPoints)
            {
                foreach (Point onePointGrid in Points)
                {
                    if ((itemPoint.Position.x + start.Position.x) == onePointGrid.Position.x && (itemPoint.Position.y + start.Position.y) == onePointGrid.Position.y && (itemPoint.Position.z + start.Position.z) == onePointGrid.Position.z)
                    {
                        onePointGrid.IsFull = true;
                        break;
                    }
                }
            }
        }
    }

    public void RemoveItemFromGrid(ItemHandler item, Point start)
    {
        List<Point> rotatedPoints = GenerateRotatedPosition(item);
        foreach (Point oneRotatedPointItem in rotatedPoints)
        {
            foreach (Point onePointGrid in Points)
            {
                if ((oneRotatedPointItem.Position.x + start.Position.x) == onePointGrid.Position.x && (oneRotatedPointItem.Position.y + start.Position.y) == onePointGrid.Position.y && (oneRotatedPointItem.Position.z + start.Position.z) == onePointGrid.Position.z)
                {
                    onePointGrid.IsFull = false;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Return true if all the point occupied by the player are free else return false
    /// </summary>
    /// <returns></returns>
    public bool CheckPointForItem(ItemHandler item, Point start)
	{
        List<Point> rotatedPoints = GenerateRotatedPosition(item);

        foreach (Point oneRotatedPointItem in rotatedPoints)
        {
            foreach (Point onePointGrid in Points)
            {
                if((oneRotatedPointItem.Position.x + start.Position.x) < 0 || (oneRotatedPointItem.Position.y + start.Position.y) <0 || (oneRotatedPointItem.Position.z + start.Position.z) <0 || (oneRotatedPointItem.Position.x + start.Position.x) > width || (oneRotatedPointItem.Position.y + start.Position.y) < height || (oneRotatedPointItem.Position.z + start.Position.z) < length)
                {
                    return true;
                }
                if ((oneRotatedPointItem.Position.x + start.Position.x) == onePointGrid.Position.x && (oneRotatedPointItem.Position.y + start.Position.y) == onePointGrid.Position.y && (oneRotatedPointItem.Position.z + start.Position.z) == onePointGrid.Position.z && onePointGrid.IsFull)
                {
                    return false;
                }
            }
        }
		return true;
    }

	private List<Point> GenerateRotatedPosition(ItemHandler item)
	{
        List<Point> rotatedPoints = new List<Point>();
        Quaternion rotationQuater = item.transform.rotation;

        rotatedPoints = GetPointAfterRotation(item.Item.Points, rotationQuater);

        return rotatedPoints;
    }

    private List<Point> GetPointAfterRotation(List<Point> points, Quaternion rotation)
	{
        List<Point> rotatedPoint = new List<Point>();
        Point tempPoint;
        if (rotation == Quaternion.identity)
            return points;
        else
        {
            foreach(Point point in points)
            {
                tempPoint = new Point(point.Position);
                tempPoint.Position = rotation * tempPoint.Position;
                tempPoint.Position = new Vector3(Mathf.Round(tempPoint.Position.x * 2) / 2, Mathf.Round(tempPoint.Position.y * 2) / 2, Mathf.Round(tempPoint.Position.z * 2) / 2);
                rotatedPoint.Add(tempPoint);
            }
        }
		return rotatedPoint;
	}
}