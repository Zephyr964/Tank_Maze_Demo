using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIToggle : CustomGUIControl
{
    public event UnityAction<bool> action;
    public bool isBool;
    private bool oldBool;


    protected override void StyleOffDraw()
    {
        isBool = GUI.Toggle(pos.Pos, isBool,content);

        if(oldBool != isBool)
        {
            action?.Invoke(isBool);
            oldBool = isBool;
        }

    }

    protected override void StyleOnDraw()
    {
        isBool = GUI.Toggle(pos.Pos, isBool, content,style);

        if (oldBool != isBool)
        {
            action?.Invoke(isBool);
            oldBool = isBool;
        }
    }
}
