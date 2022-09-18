using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPosition : MonoBehaviour
{
    private Vector3 pos;

    private Camera arCam;

    private void Awake()
    {
        arCam = Camera.main;
        pos = transform.position - arCam.transform.position;
    }

    void Update()
    {
        
    }
}
