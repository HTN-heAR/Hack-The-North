using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class TempARSpawn : MonoBehaviour
{
    public Slider distSlider;


    public GameObject sticky;
    public float distFromSelf;



    Camera arCam;

    private void Awake()
    {
        arCam = Camera.main;
    }

    private void Update()
    {
        distFromSelf = distSlider.value * 5;
    }

    public void spawnSticky()
    {
        Instantiate(sticky, arCam.transform.position + Vector3.forward * distFromSelf, sticky.transform.rotation);
    }
}
