using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;

public class GrabSticky : MonoBehaviour
{
    ARRaycastManager raycastManager;
    List<ARRaycastHit> rayHits = new List<ARRaycastHit>();

    private ARRaycastHit currentHit;

    private Vector2 touchPos;
    public float rayRange = 25f;
    private bool hold = false;

    public void OnTouchPosition(InputValue value)
    {
        touchPos = value.Get<Vector2>();
    }


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        RaycastHit hitObject;
        /**
        if (Physics.Raycast(ray, out hitObject, rayRange))
        {
            if (hitObject.transform.tag == "sticky")
            {
                hold = true;
            }
        }

        if (hold)
        {
            if(raycastManager.Raycast(touchPos, rayHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {

            }
        }
        **/

        if (raycastManager.Raycast(touchPos, rayHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose objectPose = rayHits[rayHits.Count - 1].pose;

            objectPose.position = gameObject.transform.position + Vector3.forward;
            objectPose.rotation = Quaternion.Euler(gameObject.transform.position + Vector3.up * 180);
        }
    }
}
