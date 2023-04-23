using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_DebugPanelController : MonoBehaviour
{
    public UIDocument Document;
    public KeyCode DebugShowHideKeyCode = KeyCode.D;

    public string ElementName = "TextBackground";
    public string OnScreenClass = ".rightJustified--OffScreen";
    public string OffScreenClass = ".rightJustified--OffScreen";

    public bool Shown = false;

    void Start()
    {
        SetHidden();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(DebugShowHideKeyCode))
        {
            if (Shown) {SetHidden();} else {SetShown();}
        }
    }

    void SetHidden()
    {
        Shown = false;
        VisualElement ele = Document.rootVisualElement.Q<VisualElement>(ElementName);
        ele.RemoveFromClassList(OnScreenClass);
        ele.AddToClassList(OffScreenClass);
    }
    
    void SetShown()
    {
        Shown = true;
        VisualElement ele = Document.rootVisualElement.Q<VisualElement>(ElementName);
        ele.RemoveFromClassList(OffScreenClass);
        ele.AddToClassList(OnScreenClass);
    }
}
