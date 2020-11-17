using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadControl : MonoBehaviour
{
    Camera cam;
    [SerializeField] float defaultHorizSpeed;
    [SerializeField] float defaultVertSpeed;
    [SerializeField] int minimumZoomLevel;
    [SerializeField] int maximumZoomLevel;
    [SerializeField] float zoomSpeed;
    float horizSpeed;
    float vertSpeed;
    int zoomLevel = 0;
    bool inZoomSequence;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Pivot();
        ConsiderZoom();
    }

    void Pivot()
    {
        float x = Input.GetAxis("CamHorizontal") * horizSpeed;
        float y = Input.GetAxis("CamVertical") * vertSpeed;
        transform.position += new Vector3(x, y, 0) * Time.deltaTime;

        horizSpeed = defaultHorizSpeed * Mathf.Pow(2, zoomLevel);
        vertSpeed = defaultVertSpeed * Mathf.Pow(2, zoomLevel);
    }

    void ConsiderZoom()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !inZoomSequence)
        {
            StartCoroutine("Zoom", false);
        }

        if (Input.GetKeyDown(KeyCode.RightShift) && !inZoomSequence)
        {
            StartCoroutine("Zoom", true);
        }
    }

    IEnumerator Zoom (bool zoomingIn)
    {
        inZoomSequence = true;

        // Setup

        float currentZoom = cam.orthographicSize;
        float goalZoom;

        if (zoomingIn)
        {
            if (zoomLevel != minimumZoomLevel)
            {
                goalZoom = currentZoom / 2;
                zoomLevel--;
            }
            else
            {
                goalZoom = currentZoom;
            }
        }
        else
        {
            if (zoomLevel != maximumZoomLevel)
            {
                goalZoom = currentZoom * 2;
                zoomLevel++;
            }
            else
            {
                goalZoom = currentZoom;
            }
        }

        //Animation

        while (Mathf.Abs(cam.orthographicSize - goalZoom) > 0.1f)
        {
            cam.orthographicSize += (goalZoom - cam.orthographicSize) / 4;
            yield return new WaitForSeconds(0.01f / zoomSpeed);
        }

        inZoomSequence = false;
    }
}
