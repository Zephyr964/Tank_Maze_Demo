using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2 : MonoBehaviour
{
    //鼠标的相关控制都在Cursor鼠标类中
    void Start()
    {
        //鼠标光标隐藏,flase是隐藏，true是显示
        Cursor.visible = false;

        //鼠标锁定
        //里面填的是枚举
        //None 不锁定
        //Locked 鼠标会被锁定在屏幕中央并且被隐藏，在编辑器模式下通过ESC键取消锁定
        //Confined 限制在窗口范围内，在编辑器模式下通过ESC键取消锁定
        Cursor.lockState = CursorLockMode.Locked;

        //更改鼠标图标
        //参数一 图片，参数二 偏移位置，参数三 平台支持的光标模式（默认为auto就行）
        //Cursor.SetCursor(); 
    }


    void Update()
    {
        
    }
}
