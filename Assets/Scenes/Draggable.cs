using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Plane dragPlane; 
    private Vector3 offset;
    private Camera sceneCam;
    [SerializeField] private Boolean useOffset = true;
    private void Start()
    {
        sceneCam = Camera.main;
    }

    private void OnMouseDown()
    {
        dragPlane = new Plane(sceneCam.transform.forward, transform.position);
        Ray camRay = sceneCam.ScreenPointToRay(Input.mousePosition);

        float planeDistance;
        dragPlane.Raycast(camRay, out planeDistance);
        offset = useOffset ? transform.position - camRay.GetPoint(planeDistance) : Vector3.zero;
    }
    
    void OnMouseDrag()
    {
        Ray camRay = sceneCam.ScreenPointToRay(Input.mousePosition);
        float planeDistance;
        dragPlane.Raycast(camRay, out planeDistance);
        transform.position = camRay.GetPoint(planeDistance) + offset;
    }
}
