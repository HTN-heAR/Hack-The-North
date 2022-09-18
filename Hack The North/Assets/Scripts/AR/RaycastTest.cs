using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastTest : MonoBehaviour
{
    private Vector2 touchPos;

    public void OnTouchPosition(InputValue value)
    {
        touchPos = value.Get<Vector2>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        RaycastHit hitObject;
        if (Physics.Raycast(ray, out hitObject))
        {
            Destroy(hitObject.transform.gameObject);
            
        }
    }
}
