using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadControl : MonoBehaviour
{
    enum ZoomType
    {
        In,
        Out,
    }

    //Serialized Attributes
    Camera cam;
    [SerializeField] int minimumZoomLevel;
    [SerializeField] int maximumZoomLevel;
    [SerializeField] float zoomSpeed;
    [SerializeField] float zoomFactor;
    [SerializeField] AudioSource zoomIn;
    [SerializeField] AudioSource zoomOut;

    // Other Attributes
    float defaultOrthoSize;
    int zoomLevel = 0;
    bool inZoomSequence;

    void Start()
    {
        cam = GetComponent<Camera>();
        defaultOrthoSize = cam.orthographicSize;
    }

    void LateUpdate()
    {
        ConsiderZoom();
    }

    void ConsiderZoom()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !inZoomSequence && zoomLevel != minimumZoomLevel) {
            zoomIn.Play();
            StartCoroutine("Zoom", ZoomType.In);
        }

        if (Input.GetKey(KeyCode.DownArrow) && !inZoomSequence && zoomLevel != maximumZoomLevel) {
            zoomOut.Play();
            StartCoroutine("Zoom", ZoomType.Out);
        }
    }

    IEnumerator Zoom (ZoomType zoomType)
    {
        inZoomSequence = true;
        float goalOrthoSize = GetGoalOrthoSize(zoomType);
        bool reachedGoalZoom = Mathf.Abs(cam.orthographicSize - goalOrthoSize) < 0.5f;

        while (!reachedGoalZoom)
        {
            cam.orthographicSize += (goalOrthoSize - cam.orthographicSize) / (8 / (zoomSpeed * Time.deltaTime));
            yield return new WaitForEndOfFrame();
            if (!reachedGoalZoom) reachedGoalZoom = Mathf.Abs(cam.orthographicSize - goalOrthoSize) < 0.1f;
        }

        inZoomSequence = false;
    }

    float GetGoalOrthoSize (ZoomType zoomType)
    {
        if (zoomType == ZoomType.In)
        {
            zoomLevel--;
            return cam.orthographicSize / zoomFactor;
        }
        else
        {
            zoomLevel++;
            return cam.orthographicSize * zoomFactor;
        }
    }
}
