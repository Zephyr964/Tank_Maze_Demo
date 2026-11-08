using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lesson1 : MonoBehaviour
{

    void Start()
    {
        //场景切换
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //在unity的fire中的build Setting中把场景拖入，在下列函数中输入即可切换场景 
            SceneManager.LoadScene("");

        }
    }


    void Update()
    {
        //退出游戏
        if(Input.GetKeyDown (KeyCode.Escape))
        {
            //在编辑器模式中不会执行该代码
            Application.Quit();
        }
    }
}
