using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Plane dragPlane; 
    private Vector3 offset;
    private Camera sceneCam;
    private Transform dragObj;
    private List<Transform> objects;
    [SerializeField] private Boolean useOffset = true;
    private void Start()
    {
        sceneCam = Camera.main;
        dragObj = transform.parent;
        if (dragObj == null)
        {
            dragObj = transform;
        }
    }

    private void OnMouseDown()
    {
        dragPlane = new Plane(sceneCam.transform.forward, transform.position);
        Ray camRay = sceneCam.ScreenPointToRay(Input.mousePosition);
        
        float planeDistance;
        dragPlane.Raycast(camRay, out planeDistance);
        
        offset = useOffset ? dragObj.position - camRay.GetPoint(planeDistance) : Vector3.zero;
    }
    
    void OnMouseDrag()
    {
        Ray camRay = sceneCam.ScreenPointToRay(Input.mousePosition);
        float planeDistance;
        dragPlane.Raycast(camRay, out planeDistance);
        var newPos = camRay.GetPoint(planeDistance) + offset;
        newPos = new Vector3(newPos.x, newPos.y, dragObj.position.z);
        dragObj.position = newPos;
    }
}
