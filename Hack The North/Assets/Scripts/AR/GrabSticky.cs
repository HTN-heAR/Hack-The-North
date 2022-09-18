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
    public void OnHold(InputValue value)
    {
        hold = true;
    }


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        RaycastHit hitObject;
        if (Physics.Raycast(ray, out hitObject))
        {
            if (hitObject.transform.tag == "Sticky")
            {
                //hitObject.transform.position = ray.GetPoint(holdRange);

                hitObject.transform.position = Camera.main.transform.position + transform.forward * holdRange;
            }
        }


        if (resetPos)
        {
            hitObject.transform.position = Camera.main.transform.position + transform.forward * 5;
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
