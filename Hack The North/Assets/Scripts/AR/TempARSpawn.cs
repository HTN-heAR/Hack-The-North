using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class TempARSpawn : MonoBehaviour
{


    public GameObject sticky;
    public float distFromSelf = 5f;



    Camera arCam;

    private void Awake()
    {
        arCam = Camera.main;
    }

    public void spawnSticky()
    {
        GameObject stickyObject;

        stickyObject = Instantiate(sticky, arCam.transform.position + arCam.transform.forward * distFromSelf, Quaternion.identity);
        stickyObject.transform.LookAt(arCam.transform);
    }
}
