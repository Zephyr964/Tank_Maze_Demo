using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class CustomGUIInput : CustomGUIControl
{
    private string oldStr;
    public UnityAction<string> action;

    protected override void StyleOffDraw()
    {
        content.text = GUI.TextField(pos.Pos, content.text);

        if(content.text != oldStr )
        {
            action?.Invoke(oldStr);
            oldStr = content.text;
        }
    }

    protected override void StyleOnDraw()
    {
        content.text = GUI.TextField(pos.Pos, content.text, style);


        if (content.text != oldStr)
        {
            action?.Invoke(oldStr);
            oldStr = content.text;
        }
    }
}
