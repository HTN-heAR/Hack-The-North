using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPosition : MonoBehaviour
{
    public Vector3 pos;

    private Camera arCam;

    public float speed;

    private void Awake()
    {
        arCam = Camera.main;
        pos = transform.position - arCam.transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, arCam.transform.position + pos, speed * Time.deltaTime);
    }
}
