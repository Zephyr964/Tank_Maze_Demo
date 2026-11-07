using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIButten : CustomGUIControl
{
    public event UnityAction action;

    protected override void StyleOffDraw()
    {
        if (GUI.Button(pos.Pos, content))
        {
            action?.Invoke();
        }
    }

    protected override void StyleOnDraw()
    {
        if (GUI.Button(pos.Pos, content,style))
        {
            action?.Invoke();
        }
    }
}
