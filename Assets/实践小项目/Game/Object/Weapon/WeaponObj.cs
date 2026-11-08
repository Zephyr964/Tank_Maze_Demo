using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObj : MonoBehaviour
{
    public GameObject bullet;
    public Transform[] bulletPos;

    //武器的拥有者
    public TankBaseObj fatherObj;


    public void SetFather(TankBaseObj obj)
    {
        //告诉武器拥有者是谁
        fatherObj = obj;
    }

    public void Fire()
    {
        for (int i = 0; i < bulletPos.Length; i++)
        {
            GameObject obj = Instantiate(bullet, bulletPos[i].position, bulletPos[i].rotation);
            BulletObj bulletObj = obj.GetComponent<BulletObj>();
            bulletObj.SetFather(fatherObj);
        }
    }

    
}
