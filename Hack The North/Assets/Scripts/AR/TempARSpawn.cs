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
    public Text consoleText;



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
        GameObject stickyObject;

        stickyObject = Instantiate(sticky, arCam.transform.position + arCam.transform.forward * 5, Quaternion.identity);
        stickyObject.transform.LookAt(arCam.transform);
        consoleText.text = (stickyObject.transform.eulerAngles).ToString();
        // stickyObject.transform.Rotate(0, -90, 0);
    }
}
