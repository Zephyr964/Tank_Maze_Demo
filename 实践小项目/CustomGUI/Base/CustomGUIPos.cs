using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_AlignmentType 
{
    Up,
    Down, 
    Left, 
    Right,
    Center,
    Left_Up,
    Left_Down,
    Right_Up,
    Right_Down,
}


[System.Serializable]
public class CustomGUIPos
{
    private Rect rPos = new Rect(0,0,100,100);
    public E_AlignmentType screen_AlignmentType = E_AlignmentType.Center;
    public E_AlignmentType control_AlignmentType = E_AlignmentType.Center;

    public float width = 100;//控件的宽
    public float height = 50;//控件的高
    private Vector2 centerPos;//控件的中心
    public Vector2 pos;//我们输入的偏移位置

    public Rect Pos
    {
        get
        {
            CalcCenterPos();
            Calcpos();
            rPos.width = width;
            rPos.height = height;
            return rPos;
        }
    }

    private void CalcCenterPos()
    {
        switch (control_AlignmentType) 
        {
            case E_AlignmentType.Left:
                centerPos.y = - height / 2;
                centerPos.x = 0;
                break;
            case E_AlignmentType.Right:
                centerPos.y = -height / 2;
                centerPos.x = -width;
                break;
            case E_AlignmentType.Center:
                centerPos.y = - height/2;
                centerPos.x = - width/ 2;
                break;
            case E_AlignmentType.Up:
                centerPos.y = 0;
                centerPos.x =  -width/2;
                break;
            case E_AlignmentType.Down:
                centerPos.y = -height;
                centerPos.x = -width/2;
                break;
            case E_AlignmentType.Right_Down:
                centerPos.y = -height;
                centerPos.x = -width;
                break;
            case E_AlignmentType.Right_Up:
                centerPos.y = 0;
                centerPos.x = - width;
                break;
            case E_AlignmentType.Left_Down:
                centerPos.y = -height;
                centerPos.x = 0;
                break;
            case E_AlignmentType.Left_Up:
                centerPos.y = 0;
                centerPos.x = 0;
                break;
        }

    }

    private void Calcpos()
    {
        switch (screen_AlignmentType)
        {
            case E_AlignmentType.Left:
                rPos.x = 0 + centerPos.x + pos.x;
                rPos.y = Screen.height/2 + centerPos.y + pos.y;
                break;
            case E_AlignmentType.Right:
                rPos.x = Screen.width + centerPos.x - pos.x;
                rPos.y = Screen.height/2 + centerPos.y + pos.y;
                break;
            case E_AlignmentType.Center:
                rPos.x = Screen.width / 2 + centerPos.x + pos.x;
                rPos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_AlignmentType.Up:
                rPos.x = Screen.width / 2 + centerPos.x + pos.x;
                rPos.y = 0 + centerPos.y + pos.y;
                break;
            case E_AlignmentType.Down:
                rPos.x = Screen.width / 2 + centerPos.x + pos.x;
                rPos.y = Screen.height + centerPos.y - pos.y;
                break;
            case E_AlignmentType.Right_Down:
                rPos.x = Screen.width + centerPos.x - pos.x;
                rPos.y = Screen.height + centerPos.y - pos.y;
                break;
            case E_AlignmentType.Right_Up:
                rPos.x = Screen.width + centerPos.x - pos.x;
                rPos.y = 0+ centerPos.y + pos.y;
                break;
            case E_AlignmentType.Left_Down:
                rPos.x = 0 + centerPos.x + pos.x;
                rPos.y = Screen.height + centerPos.y - pos.y;
                break;
            case E_AlignmentType.Left_Up:
                rPos.x = 0 + centerPos.x + pos.x;
                rPos.y = 0 + centerPos.y + pos.y;
                break;
        }
    }

}
