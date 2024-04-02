using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ZHandler : MonoBehaviour
{
    private List<GameObject> dragObjs;
    private GameObject lastSelected;
    private Camera sceneCam;
    [SerializeField] int bottomZPos = 0;
    [SerializeField] string searchTag = "Draggable";

    void Start()
    {
        sceneCam = Camera.main;
        dragObjs = new List<GameObject>();
        var searchArr = GameObject.FindGameObjectsWithTag(searchTag);
        dragObjs.AddRange(searchArr);
        Redraw();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void Redraw()
    {
        dragObjs.Sort(isHigher);
        var names = dragObjs.Select(x => x.name);
        string order = string.Join(',', names);
        Debug.Log($"Sorted dragObjs to: {order}");
        
        int topZ = bottomZPos;
        foreach (GameObject obj in dragObjs)
        {
            var pos = obj.transform.position;
            obj.transform.position = new Vector3(pos.x, pos.y, topZ);
            topZ--;
        }
    }

    private void HandleClick()
    {
        Ray camRay = sceneCam.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(camRay.origin, camRay.direction);
        
        if (!hit)
        {
            return;
        }
        
        var selectedObj = hit.collider.transform;
        Transform parent = selectedObj.transform.parent;
        
        //gets parent object of collider
        while (parent != null)
        {
            selectedObj = parent.transform;
            parent = selectedObj.parent;
        }

        //check if correct tag
        if (!selectedObj.CompareTag(searchTag))
            return;

        lastSelected = selectedObj.GameObject();
        Debug.Log($"Raycasted. Hit {lastSelected.name}.");

        // dragObjs.Remove(lastSelected);
        // dragObjs.Add(lastSelected);

        var pos = lastSelected.transform.position;
        lastSelected.transform.position = new Vector3(pos.x, pos.y, bottomZPos - dragObjs.Count);
        Debug.Log($"new z val: {bottomZPos - dragObjs.Count}");
        
        Redraw();
    }
    
    private static int isHigher(GameObject obj1, GameObject obj2)
    {
        var z1 = obj1.transform.position.z;
        var z2 = obj2.transform.position.z;
        return z2.CompareTo(z1);
    }
}
