using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private ItemHandler currentlyHandle;

    [SerializeField] Suitcase grid;

    [SerializeField] bool rotation;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && currentlyHandle == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("Hit : " + hit.transform.name);
                if (hit.transform.GetComponentInParent<ItemHandler>() != null)
                {
                    currentlyHandle = hit.transform.GetComponentInParent<ItemHandler>();
                    PutItemOnTheGrid();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveX(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveX(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            MoveY(1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            MoveY(-1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveZ(1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveZ(-1);
        }
        else if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift) && rotation)
        {
            currentlyHandle.transform.rotation = Quaternion.Euler(new Vector3(currentlyHandle.transform.rotation.eulerAngles.x, currentlyHandle.transform.rotation.eulerAngles.y, currentlyHandle.transform.rotation.eulerAngles.z + 90f));
        }
        else if (Input.GetKeyDown(KeyCode.R) && rotation)
        {
            currentlyHandle.transform.rotation = Quaternion.Euler(new Vector3(currentlyHandle.transform.rotation.eulerAngles.x, currentlyHandle.transform.rotation.eulerAngles.y + 90f, currentlyHandle.transform.rotation.eulerAngles.z));
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            PlaceItem();
            currentlyHandle = null;
        }

    }


    private void PutItemOnTheGrid()
    {
        foreach(Point point in grid.Points)
        {
            if (!point.IsFull)
            {
                currentlyHandle.CurrentPoint = point;
                currentlyHandle.transform.position = point.Position;
                return;
            }
        }
        Debug.Log("No point available in the suitcase");
    }


    private void MoveX(int axis)
    {
        for (int i = 1; i < 5; i++)
        {
            Vector3 pointPositionAdapted;
            pointPositionAdapted = new Vector3(currentlyHandle.CurrentPoint.Position.x + (i * axis), currentlyHandle.CurrentPoint.Position.y, currentlyHandle.CurrentPoint.Position.z);
            Point newPoint = grid.Points.Find(_ => _.Position == pointPositionAdapted && !_.IsFull);
            if (newPoint != null)
            {
                currentlyHandle.CurrentPoint = newPoint;
                currentlyHandle.transform.position = newPoint.Position;
                return;
            }
        }
        Debug.Log("No available point on the left");
    }

    private void MoveY(int axis)
    {
        Vector3 pointPositionAdapted;
        for (int i = 1; i < 5; i++)
        {
            pointPositionAdapted = new Vector3(currentlyHandle.CurrentPoint.Position.x, currentlyHandle.CurrentPoint.Position.y + (i * axis), currentlyHandle.CurrentPoint.Position.z);
            Point newPoint = grid.Points.Find(_ => _.Position == pointPositionAdapted && !_.IsFull);
            if (newPoint != null)
            {
                currentlyHandle.CurrentPoint = newPoint;
                currentlyHandle.transform.position = newPoint.Position;
                return;
            }
        }
        Debug.Log("No available point on the left");
    }

    private void MoveZ(int axis)
    {
        Vector3 pointPositionAdapted;
        for (int i = 1; i < 5; i++)
        {
            pointPositionAdapted = new Vector3(currentlyHandle.CurrentPoint.Position.x, currentlyHandle.CurrentPoint.Position.y, currentlyHandle.CurrentPoint.Position.z + (i * axis));
            Point newPoint = grid.Points.Find(_ => _.Position == pointPositionAdapted && !_.IsFull);
            if (newPoint != null)
            {
                currentlyHandle.CurrentPoint = newPoint;
                currentlyHandle.transform.position = newPoint.Position;
                return;
            }
        }
        Debug.Log("No available point on the left");
    }

    public void PlaceItem()
    {
        grid.ContainsFullPoint(currentlyHandle.Item, currentlyHandle.CurrentPoint);
    }
}
