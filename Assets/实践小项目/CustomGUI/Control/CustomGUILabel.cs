using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class CustomGUILabel : CustomGUIControl
{

    protected override void StyleOffDraw()
    {
        GUI.Label(pos.Pos, content);
    }

    protected override void StyleOnDraw()
    {
        GUI.Label(pos.Pos, content, style);
    }

}
