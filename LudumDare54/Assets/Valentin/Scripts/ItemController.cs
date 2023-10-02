using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class ItemController : MonoBehaviour
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
                    AudioManagerCustom.Instance.PlayClip("SFX_click");
                    currentlyHandle = hit.transform.GetComponentInParent<ItemHandler>();
                    if(currentlyHandle.CurrentPoint != null)
                        grid.RemoveItemFromGrid(currentlyHandle, currentlyHandle.CurrentPoint);
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
            currentlyHandle.transform.Rotate(0, 0, 90);
            AudioManagerCustom.Instance.PlayClip("SFX_slouch");
        }
        else if (Input.GetKeyDown(KeyCode.R) && rotation)
        {
            currentlyHandle.transform.Rotate(0, 90, 0);
            AudioManagerCustom.Instance.PlayClip("SFX_slouch");
        }
        else if (Input.GetKeyDown(KeyCode.E) && rotation)
        {
            currentlyHandle.transform.Rotate(90, 0, 0);
            AudioManagerCustom.Instance.PlayClip("SFX_slouch");
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            PlaceItem();
            currentlyHandle = null;
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            ResetItem(currentlyHandle);
            AudioManagerCustom.Instance.PlayClip("SFX_reset");
        }

    }


    private void PutItemOnTheGrid(Point _point = null)
    {
        if(_point != null)
        {
            currentlyHandle.CurrentPoint = _point;
            currentlyHandle.transform.position = _point.Position;
            return;
        }

        foreach(Point point in grid.Points)
        {
            if (grid.CheckPointForItem(currentlyHandle, point))
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
            if (newPoint != null && grid.CheckPointForItem(currentlyHandle, newPoint))
            {
                currentlyHandle.CurrentPoint = newPoint;
                currentlyHandle.transform.position = newPoint.Position;
                AudioManagerCustom.Instance.PlayClip("SFX_slide");
                return;
            }
        }
        if(axis == -1)
            Debug.Log("No available point on the left");
        else
            Debug.Log("No available point on the right");
    }

    private void MoveY(int axis)
    {
        Vector3 pointPositionAdapted;
        for (int i = 1; i < 5; i++)
        {
            pointPositionAdapted = new Vector3(currentlyHandle.CurrentPoint.Position.x, currentlyHandle.CurrentPoint.Position.y + (i * axis), currentlyHandle.CurrentPoint.Position.z);
            Point newPoint = grid.Points.Find(_ => _.Position == pointPositionAdapted && !_.IsFull);
            if (newPoint != null && grid.CheckPointForItem(currentlyHandle, newPoint))
            {
                currentlyHandle.CurrentPoint = newPoint;
                currentlyHandle.transform.position = newPoint.Position;
                AudioManagerCustom.Instance.PlayClip("SFX_slide");
                return;
            }
        }
        if (axis == -1)
            Debug.Log("No available point on the down");
        else
            Debug.Log("No available point on the up");
    }

    private void MoveZ(int axis)
    {
        Vector3 pointPositionAdapted;
        for (int i = 1; i < 5; i++)
        {
            pointPositionAdapted = new Vector3(currentlyHandle.CurrentPoint.Position.x, currentlyHandle.CurrentPoint.Position.y, currentlyHandle.CurrentPoint.Position.z + (i * axis));
            Point newPoint = grid.Points.Find(_ => _.Position == pointPositionAdapted && !_.IsFull);
            if (newPoint != null && grid.CheckPointForItem(currentlyHandle, newPoint))
            {
                currentlyHandle.CurrentPoint = newPoint;
                currentlyHandle.transform.position = newPoint.Position;
                AudioManagerCustom.Instance.PlayClip("SFX_slide");
                return;
            }
        }
        if (axis == -1)
            Debug.Log("No available point backward");
        else
            Debug.Log("No available point frontward");
    }

    public void PlaceItem()
    {
        grid.ContainsFullPoint(currentlyHandle, currentlyHandle.CurrentPoint);
        AudioManagerCustom.Instance.PlayClip("SFX_drop");
    }

    public void ResetItem(ItemHandler item)
    {
        item.transform.SetPositionAndRotation(item.StartPosition, item.StartRotation);
        currentlyHandle = null;
    }
}
