using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_Slider 
{
    Horizontal,
    Vertical,
}


public class CustomGUISilder : CustomGUIControl
{
    public E_Slider sliderType = E_Slider.Horizontal;
    public float maxIndex = 1;
    public float minIndex = 0;
    public float nowIndex;
    public GUIStyle nullStyle;

    public UnityAction<float> action;
    private float oldIndex;
    protected override void StyleOffDraw()
    {
        switch (sliderType) 
        {
            case E_Slider.Horizontal:
                nowIndex = GUI.HorizontalSlider(pos.Pos, nowIndex, minIndex, maxIndex);
                break;
            case E_Slider.Vertical:
                nowIndex = GUI.VerticalSlider(pos.Pos, nowIndex, minIndex, maxIndex);
                break;
        }

        if(oldIndex != nowIndex)
        {
            action?.Invoke(oldIndex);
            oldIndex = nowIndex;
        }

    }

    protected override void StyleOnDraw()
    {
        switch (sliderType)
        {
            case E_Slider.Horizontal:
                nowIndex = GUI.HorizontalSlider(pos.Pos, nowIndex, minIndex, maxIndex, style, nullStyle);
                break;
            case E_Slider.Vertical:
                nowIndex = GUI.VerticalSlider(pos.Pos, nowIndex, minIndex, maxIndex, style, nullStyle);
                break;
        }

        if (oldIndex != nowIndex)
        {
            action?.Invoke(oldIndex);
            oldIndex = nowIndex;
        }
    }
}
