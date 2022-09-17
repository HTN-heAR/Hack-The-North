using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempARSpawn : MonoBehaviour
{
    public Slider distSlider;


    public GameObject sticky;
    public float distFromSelf;

    private void Update()
    {
        distFromSelf = distSlider.value * 5;
    }

    public void spawnSticky()
    {
        Instantiate(sticky, Vector3.forward * distFromSelf, Quaternion.identity);
    }
}
