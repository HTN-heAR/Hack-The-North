using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TranscriptButton : MonoBehaviour
{
    private RectTransform rectTransform;
    public RectTransform canvasTransform;
    public RectTransform TopPanel;
    private Vector3 startPos;
    private Vector3 startPosTop;

    public float expandSpeed;

    private bool expanding = false;
    private bool open = false;

    public void Minimize()
    {
        expanding = false;
        open = false;
    }

    public void Expand()
    {
        expanding = true;
    }

    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        startPos = rectTransform.position;
        startPosTop = rectTransform.position;
    }

    private void Update()
    {
        if (expanding)
        {
            rectTransform.position = Vector3.Lerp(rectTransform.position, startPos + Vector3.up * (canvasTransform.rect.height - 287), Time.deltaTime * expandSpeed);
            // TopPanel.position = Vector3.Lerp(rectTransform.position, startPos + Vector3.down * 150f, Time.deltaTime * expandSpeed);
            if (rectTransform.position.y >= startPos.y + canvasTransform.rect.height)
            {
                expanding = false;
                open = true;
            }
        }


        if (!expanding && !open)
        {
            // TopPanel.position = Vector3.Lerp(TopPanel.position, startPosTop, Time.deltaTime * expandSpeed);
            rectTransform.position = Vector3.Lerp(rectTransform.position, startPos, Time.deltaTime * expandSpeed);
        }
    }
}
