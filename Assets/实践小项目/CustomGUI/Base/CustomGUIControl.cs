using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_StyleOnOff
{
    Off,
    OnOff,
}

public abstract class CustomGUIControl : MonoBehaviour
{
    public CustomGUIPos pos;
    public GUIContent content;
    public GUIStyle style;
    public E_StyleOnOff onOff = E_StyleOnOff.Off;


    public void DrawGUI()
    {
        //修复我自身报错的问题
        if (this == null || !this) return;

        switch (onOff) 
        {
            case E_StyleOnOff.Off:
                StyleOffDraw();
                break;
            case E_StyleOnOff.OnOff:
                StyleOnDraw();
                break;
        }

    }

    protected abstract void StyleOffDraw();

    protected abstract void StyleOnDraw();


}
