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
    public float holdRange = 2f;
    private bool hold = false;
    private bool resetPos = false;

    public void OnTouchPosition(InputValue value)
    {
        touchPos = value.Get<Vector2>();
    }
    public void OnReleaseTouch(InputValue value)
    {
        hold = false;
        resetPos = true;
    }


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        RaycastHit hitObject;
        if (Physics.Raycast(ray, out hitObject, rayRange))
        {
            if (hitObject.transform.tag == "sticky")
            {
                hold = true;
            }
        }

        if (hold)
        {
            hitObject.transform.position = ray.GetPoint(holdRange);
        }
        if (resetPos)
        {
            hitObject.transform.GetComponent<StickyPosition>().pos = hitObject.transform.position - Camera.main.transform.position;
        }

        /**
        if (raycastManager.Raycast(touchPos, rayHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose objectPose = rayHits[rayHits.Count - 1].pose;

            objectPose.position = gameObject.transform.position + Vector3.forward;
            objectPose.rotation = Quaternion.Euler(gameObject.transform.position + Vector3.up * 180);
        }
        **/
    }
}
