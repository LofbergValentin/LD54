using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private ItemHandler currentlyHandle;
    private Vector3 currentlyHandleTransform;

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

                    currentlyHandleTransform = currentlyHandle.transform.position;
                    if (currentlyHandleTransform.x < minX)
                    {
                        currentlyHandleTransform = new Vector3(minX, currentlyHandleTransform.y, currentlyHandleTransform.z);
                    }
                    else if (currentlyHandleTransform.x > maxX)
                    {
                        currentlyHandleTransform = new Vector3(maxX, currentlyHandleTransform.y, currentlyHandleTransform.z);
                    }

                    if (currentlyHandleTransform.y < minY)
                    {
                        currentlyHandleTransform = new Vector3(currentlyHandleTransform.x, minY, currentlyHandleTransform.z);
                    }
                    else if (currentlyHandleTransform.y > maxY)
                    {
                        currentlyHandleTransform = new Vector3(currentlyHandleTransform.x, maxY, currentlyHandleTransform.z);
                    }

                    if (currentlyHandleTransform.z < minZ)
                    {
                        currentlyHandleTransform = new Vector3(currentlyHandleTransform.x, currentlyHandleTransform.y, minZ);
                    }
                    else if (currentlyHandleTransform.z > maxZ)
                    {
                        currentlyHandleTransform = new Vector3(currentlyHandleTransform.x, currentlyHandleTransform.y, maxZ);
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
                currentlyHandle = null;
            }
        }
    }
}
