using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    private static ToolTip instance;
    [SerializeField] private string startText = "Placeholder Name";
    private TextMeshPro tipText;
    [SerializeField] private float xOffset = 2f;
    [SerializeField] private float yOffset = 2f;
    [SerializeField] private float xScaling = 1f;
    [SerializeField] private float yScaling = 0.5f;
    [SerializeField] private float xPadding = 1f;
    [SerializeField] private float yPadding = 1f;
    [SerializeField] private string bgChildName = "Tip Background";
    [SerializeField] private string textChildName = "Tip Text";
    private RectTransform bg;

    private Camera sceneCam;

    void Awake()
    {
        instance = this;
        sceneCam = Camera.main;
        bg = transform.Find(bgChildName).GetComponent<RectTransform>();
        tipText = transform.Find(textChildName).GetComponent<TextMeshPro>();
        setInactive();
    }
    
    void Update()
    {
        //setActive(startText);
        if (gameObject.activeSelf)
        {
            Vector3 mousePos = sceneCam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(
                mousePos.x + (xOffset * 0.1f),
                mousePos.y + (yOffset * 0.1f),
                transform.position.z
            );
        }
    }
    
    private void setActivePrivate(string text)
    {
        gameObject.SetActive(true);
        tipText.text = text;
        
        if (tipText == null || bg == null)
        {
            Debug.LogError("References not set properly.");
            return;
        }
        
        Vector2 bgSize = new Vector2(
            tipText.preferredWidth + (xPadding * 2f), 
            tipText.preferredHeight + (yPadding * 2f)
        );
        bg.sizeDelta = bgSize;
    }
    
    private void setInactivePrivate()
    {
        gameObject.SetActive(false);
    }

    public static void setActive(string text)
    {
        instance.setActivePrivate(text);
    }
    
    public static void setInactive()
    {
        instance.setInactivePrivate();
    }
}
