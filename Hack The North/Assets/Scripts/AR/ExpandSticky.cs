using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExpandSticky : MonoBehaviour
{

    private Vector2 touchPos;
    public float rayRange = 25f;
    public float magnifyProximity = 1f;
    public float magnifySpeed = 5f;
    private GameObject currentObject;

    private bool focus = false;
    private bool magnify = false;
    private float initialDist;

    public void OnTouchPosition(InputValue value)
    {
        touchPos = value.Get<Vector2>();
    }

    public void OnClick()
    {
        if (!focus)
        {
            magnify = true;
        }
        if (currentObject = null)
        {
            magnify = false;
            focus = false;
        }

    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        RaycastHit hitObject;
        if (Physics.Raycast(ray, out hitObject, rayRange))
        {
            if (hitObject.transform.tag == "Sticky")
            {
                currentObject = hitObject.transform.gameObject;
                initialDist = Vector3.Distance(currentObject.transform.position, Camera.main.transform.position);
            }
            else
            {
                currentObject = null;
                return;
            }
        }
        else
        {
            currentObject = null;
            return;
        }

        if (magnify)
        {
            if (currentObject != null)
            {
                currentObject.transform.position = Vector3.Lerp(currentObject.transform.position, Camera.main.transform.position + transform.forward * magnifyProximity, magnifySpeed * Time.deltaTime);

                if (Vector3.Distance(currentObject.transform.position, Camera.main.transform.position) <= magnifyProximity)
                {
                    magnify = false;
                    focus = true;
                }
            }
        }
        else if (!magnify && !focus)
        {
            if (Vector3.Distance(currentObject.transform.position, Camera.main.transform.position) < initialDist)
            {
                currentObject.transform.position = Vector3.Lerp(currentObject.transform.position, Camera.main.transform.position + transform.forward * initialDist, magnifySpeed * Time.deltaTime);
            }
        }


    }
}
