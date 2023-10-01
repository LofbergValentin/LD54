using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera _cam;
    [SerializeField] float zoomScale;
    CinemachineTransposer transposer;

    [SerializeField] float camZoomInMax, camZoomOutMax; 

    private void Start()
    {
        transposer = _cam.GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.mouseScrollDelta.y > 0 && transposer.m_FollowOffset.x < camZoomOutMax) || (Input.mouseScrollDelta.y < 0 && transposer.m_FollowOffset.x > camZoomInMax))
        {
            transposer.m_FollowOffset = new Vector3(transposer.m_FollowOffset.x + (Input.mouseScrollDelta.y * zoomScale), transposer.m_FollowOffset.y, transposer.m_FollowOffset.z);
        }

        if (Input.GetMouseButton(1) || GameManager.Instance.Finished)
        {
            _cam.enabled = true;
        }
        else if (_cam.enabled == true )
        {
            _cam.enabled = false;
        }
    }
}
