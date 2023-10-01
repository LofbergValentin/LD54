using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private ItemHandler currentlyHandle;
    private Vector3 currentlyHandlePosition;

    [SerializeField] bool rotation;

    public float moveSpeed = 5f; // Adjust the speed of movement.
    public float minX = -5f;     // Minimum X-axis position.
    public float maxX = 5f;      // Maximum X-axis position.

    public float minY = -5f;     // Minimum Z-axis position.
    public float maxY = 5f;      // Maximum Z-axis position.

    public float minZ = -5f;     // Minimum Z-axis position.
    public float maxZ = 5f;      // Maximum Z-axis position.

    float timeClicked;
    float ellapsedTime;

    // Update is called once per frame
    void Update()
    {
        ellapsedTime += Time.deltaTime;
        if(Input.GetMouseButtonUp(0) && currentlyHandle == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("Hit : " + hit.transform.name);
                if(hit.transform.GetComponentInParent<ItemHandler>() != null)
                {
                    currentlyHandle = hit.transform.GetComponentInParent<ItemHandler>();
                    timeClicked = ellapsedTime;

                    currentlyHandlePosition = currentlyHandle.transform.position;
                    if (currentlyHandlePosition.x < minX)
                    {
                        currentlyHandlePosition = new Vector3(minX, currentlyHandlePosition.y, currentlyHandlePosition.z);
                    }
                    else if (currentlyHandlePosition.x > maxX)
                    {
                        currentlyHandlePosition = new Vector3(maxX, currentlyHandlePosition.y, currentlyHandlePosition.z);
                    }

                    if (currentlyHandlePosition.y < minY)
                    {
                        currentlyHandlePosition = new Vector3(currentlyHandlePosition.x, minY, currentlyHandlePosition.z);
                    }
                    else if (currentlyHandlePosition.y > maxY)
                    {
                        currentlyHandlePosition = new Vector3(currentlyHandlePosition.x, maxY, currentlyHandlePosition.z);
                    }

                    if (currentlyHandlePosition.z < minZ)
                    {
                        currentlyHandlePosition = new Vector3(currentlyHandlePosition.x, currentlyHandlePosition.y, minZ);
                    }
                    else if (currentlyHandlePosition.z > maxZ)
                    {
                        currentlyHandlePosition = new Vector3(currentlyHandlePosition.x, currentlyHandlePosition.y, maxZ);
                    }
                }
            }
        }

        if (currentlyHandle != null)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                // Get the mouse position in screen space.
                Vector3 mousePosition = Input.mousePosition;

                // Convert the mouse position from screen space to world space.
                Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

                // Clamp the X-axis and Y-axis positions to the specified ranges.
                float xPosition = Mathf.Clamp(worldMousePosition.x, minX, maxX);
                float yPosition = Mathf.Clamp(worldMousePosition.y, minY, maxY);

                // Set the new position for the GameObject, affecting both X and Y axes.
                currentlyHandle.transform.position = new Vector3(xPosition, yPosition, currentlyHandle.transform.position.z);
            }
            else if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift) && rotation)
            {
                currentlyHandle.transform.rotation = Quaternion.Euler(new Vector3(currentlyHandle.transform.rotation.eulerAngles.x, currentlyHandle.transform.rotation.eulerAngles.y, currentlyHandle.transform.rotation.eulerAngles.z + 90f));
            }
            else if (Input.GetKeyDown(KeyCode.R) && rotation)
            {
                currentlyHandle.transform.rotation = Quaternion.Euler(new Vector3(currentlyHandle.transform.rotation.eulerAngles.x, currentlyHandle.transform.rotation.eulerAngles.y + 90f, currentlyHandle.transform.rotation.eulerAngles.z));
            }
            else
            {
                // Get the mouse position in screen space.
                Vector3 mousePosition = Input.mousePosition;

                // Convert the mouse position from screen space to world space.
                Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

                // Clamp the X-axis and Z-axis positions to the specified ranges.
                float xPosition = Mathf.Clamp(worldMousePosition.x, minX, maxX);
                float zPosition = Mathf.Clamp(worldMousePosition.z, minZ, maxZ);

                // Set the new position for the GameObject, affecting both X and Z axes.
                currentlyHandle.transform.position = new Vector3(xPosition, currentlyHandle.transform.position.y, zPosition);
            }

            if (Input.GetMouseButtonUp(0) && timeClicked + 0.1f < ellapsedTime)
            {
                Debug.Log("Release item");
                if(SnapToGrid(currentlyHandle.transform.position))
                {
                    currentlyHandle = null;
                }
            }
        }
    }

    private bool SnapToGrid(Vector3 itemPosition)
    {
        List<Point> validPoints = new List<Point>();

        Bounds bound = new Bounds(itemPosition,Vector3.one);
        foreach(Point point in GameManager.Instance.valise.Points)
        {
            if (bound.Contains(point.Position))
            {
                validPoints.Add(point);
            }
        }

        if(validPoints.Count == 1)
        {
            currentlyHandle.transform.position = validPoints[0].Position;
            GameManager.Instance.SetItemIntoSuitcase(currentlyHandle.Item, validPoints[0]);
            return true;
        }
        else
        {
            Debug.Log("No point available here");
            return false;
        }
    }
}
