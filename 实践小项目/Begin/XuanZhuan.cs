using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Rotate 
{
    x, y, z
}


public class XuanZhuan : MonoBehaviour
{
    public float TransformSpeed = 5;
    public E_Rotate e_Rotate;

    void Update()
    {
        switch (e_Rotate) 
        {
            case E_Rotate.x:
                transform.Rotate(TransformSpeed * Time.deltaTime, 0,0 );
                break;
            case E_Rotate.y:
                transform.Rotate(0, TransformSpeed * Time.deltaTime, 0);
                break;
            case E_Rotate.z:
                transform.Rotate(0, 0,TransformSpeed * Time.deltaTime);
                break;
        }



    }

}
