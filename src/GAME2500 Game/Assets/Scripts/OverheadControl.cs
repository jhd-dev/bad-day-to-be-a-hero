﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadControl : MonoBehaviour
{
    enum ZoomType
    {
        In,
        Out,
        OnMinion
    }

    //Serialized Attributes
    Camera cam;
    [Header("Pivoting")]
    [SerializeField] float defaultHorizSpeed;
    [SerializeField] float defaultVertSpeed;
    [Header("Zooming")]
    [SerializeField] int minimumZoomLevel;
    [SerializeField] int maximumZoomLevel;
    [SerializeField] float zoomSpeed;
    [SerializeField] AudioSource zoomIn;
    [SerializeField] AudioSource zoomOut;
    [SerializeField] AudioSource centerIn;
    [Header("Switching")]
    [SerializeField] Soul soul;

    // Other Attributes
    float actualHorizSpeed;
    float actualVertSpeed;
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
        //if (ControlCenter.inCameraMode)
        //{
           // if (!inZoomSequence) Pivot();
        ConsiderPossession();
        //}

        ConsiderZoom();
    }

    void Pivot()
    {
        actualHorizSpeed = defaultHorizSpeed * Mathf.Pow(2, zoomLevel);
        actualVertSpeed = defaultVertSpeed * Mathf.Pow(2, zoomLevel);
        float x = Input.GetAxis("CamHorizontal") * actualHorizSpeed;
        float y = Input.GetAxis("CamVertical") * actualVertSpeed;
        transform.position += new Vector3(x, y, 0) * Time.deltaTime;
    }

    void ConsiderPossession()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space) && !inZoomSequence) {
            centerIn.Play();
            StartCoroutine("Zoom", ZoomType.OnMinion);
        }
        */
    }

    void ConsiderZoom()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !inZoomSequence && zoomLevel != minimumZoomLevel) {
            zoomIn.Play();
            StartCoroutine("Zoom", ZoomType.In);
            //ControlCenter.inCameraMode = true;
        }

        if (Input.GetKey(KeyCode.DownArrow) && !inZoomSequence && zoomLevel != maximumZoomLevel) {
            zoomOut.Play();
            StartCoroutine("Zoom", ZoomType.Out);
            //ControlCenter.inCameraMode = true;
        }
    }

    IEnumerator Zoom (ZoomType zoomType)
    {
        // Setup

        inZoomSequence = true;
        float goalOrthoSize = GetGoalOrthoSize(zoomType);
        //Vector3 goalPos = GetGoalPos(zoomType);

        if (zoomType == ZoomType.OnMinion)
        {
            soul.SetHost(GetClosestMinion().GetComponent<Villain>());
        }

        // Animation

        bool reachedGoalZoom = Mathf.Abs(cam.orthographicSize - goalOrthoSize) < 0.5f;
        //bool reachedGoalPos = (transform.position - goalPos).magnitude < 0.1f;

        while (!reachedGoalZoom) //|| !reachedGoalPos)
        {
            //transform.position += (goalPos - transform.position) / (8 / (zoomSpeed * Time.deltaTime));
            cam.orthographicSize += (goalOrthoSize - cam.orthographicSize) / (8 / (zoomSpeed * Time.deltaTime));
            yield return new WaitForEndOfFrame();
            if (!reachedGoalZoom) reachedGoalZoom = Mathf.Abs(cam.orthographicSize - goalOrthoSize) < 0.5f;
            //if (!reachedGoalPos) reachedGoalPos = (transform.position - goalPos).magnitude < 0.1f;
        }

        /*
        if (zoomType == ZoomType.OnMinion)
        {
            //transform.parent = soul.transform;
            //ControlCenter.inCameraMode = false;
        }
        else
        {
            //transform.parent = null;
        }
        */

        inZoomSequence = false;
    }

    float GetGoalOrthoSize (ZoomType zoomType)
    {
        if (zoomType == ZoomType.In)
        {
            zoomLevel--;
            return cam.orthographicSize / 2;
        }
        else if (zoomType == ZoomType.Out)
        {
            zoomLevel++;
            return cam.orthographicSize * 2;
        }
        else //zooming onto a minion to control
        {
            zoomLevel = 0;
            return defaultOrthoSize;
        }
    }

    /*
    Vector3 GetGoalPos(ZoomType zoomType)
    {
        if (zoomType == ZoomType.OnMinion)
        {
            return GetClosestMinion().transform.TransformPoint(Vector3.zero);
        }

        return transform.position;
    }
    */

    GameObject GetClosestMinion()
    {
        List<GameObject> minions = new List<GameObject>(GameObject.FindGameObjectsWithTag("Minion"));
        minions.Add(GameObject.FindGameObjectWithTag("Boss"));
        Vector2 midScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        float minDistance = 99999;
        int closestIndex = -1;

        for (int i = 0; i < minions.Count; i++)
        {
            Vector2 minionPos = cam.WorldToScreenPoint(minions[i].transform.position);
            float distance = Vector2.Distance(minionPos, midScreen);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        return minions[closestIndex];
    }
}
