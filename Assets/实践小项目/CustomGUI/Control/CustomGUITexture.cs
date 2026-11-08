using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUITexture : CustomGUIControl
{
    public ScaleMode scaleMode = ScaleMode.ScaleToFit;
    protected override void StyleOffDraw()
    {
        GUI.DrawTexture(pos.Pos, content.image,scaleMode);
    }

    protected override void StyleOnDraw()
    {
        GUI.DrawTexture(pos.Pos, content.image, scaleMode);
    }
}
