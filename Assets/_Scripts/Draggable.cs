using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Plane dragPlane; 
    private Vector3 offset;
    private Camera sceneCam;
    private Transform parent;
    private List<Transform> objects;
    [SerializeField] private Boolean useOffset = true;
    private void Start()
    {
        sceneCam = Camera.main;
        parent = transform.parent;
    }

    private void OnMouseDown()
    {
        dragPlane = new Plane(sceneCam.transform.forward, transform.position);
        Ray camRay = sceneCam.ScreenPointToRay(Input.mousePosition);
        
        float planeDistance;
        dragPlane.Raycast(camRay, out planeDistance);
        
        offset = useOffset ? parent.position - camRay.GetPoint(planeDistance) : Vector3.zero;
    }
    
    void OnMouseDrag()
    {
        Ray camRay = sceneCam.ScreenPointToRay(Input.mousePosition);
        float planeDistance;
        dragPlane.Raycast(camRay, out planeDistance);
        parent.position = camRay.GetPoint(planeDistance) + offset;
    }
}
