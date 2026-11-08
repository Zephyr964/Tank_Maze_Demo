using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lesson3 : MonoBehaviour
{

    void Start()
    {
        #region 随机数
        //在unity中有自己写的随机数，要用C#中的随机数的话，在使用过程中前面加System这个命名空间

        //c#
        System.Random random = new System.Random();
        random.Next(0, 100);

        //Unity
        //
        int index = Random.Range(0, 100);//整数重载必须是整数
        float floatIndex = Random.Range(0.1f, 100.1f);//浮点数重载只要任意一个数是就可以了

        #endregion

        #region 委托

        #region C#
        //C#

        //无参
        System.Action action = () =>
        {

        };
        System.Action<int, string> action2 = (i, s) =>
        {

        };

        //有参
        System.Func<string> func = () =>
        {
            return "";
        };

        System.Func<int, string> func2 = (i) =>
        {
            return "";//多参数，返回值填在最后
        };
        #endregion
        //Unity
        //它只有这种无返回值委托
        UnityAction uact = () =>
        {
            
        };
        UnityAction<string> uact2 = (s) =>
        {

        };

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
