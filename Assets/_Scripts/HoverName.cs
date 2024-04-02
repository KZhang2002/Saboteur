using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class HoverName : MonoBehaviour
{
    [SerializeField] public string ttName = "Name Placeholder";

    void OnMouseOver()
    {
        ToolTip.setActive(ttName);
    }

    private void OnMouseExit()
    {
        ToolTip.setInactive();
    }
}
