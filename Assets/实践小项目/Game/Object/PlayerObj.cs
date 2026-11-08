using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : TankBaseObj
{
    public WeaponObj nowWeapon;
    public Transform weaponPos;

    public int bulletNum = 0;//目前的子弹数
    private int bulletSuiPian = 0;//10个碎片合成一个子弹


    void Start()
    {
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * MoveSpeed * 3 * Time.deltaTime);
        }
        else
        {
            //移动
            this.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * MoveSpeed * Time.deltaTime);
        }

        //旋转
        this.transform.Rotate(Input.GetAxis("Horizontal") * Vector3.up * Time.deltaTime * RotateSpeed);
        //头旋转
        tankHead.transform.Rotate(Input.GetAxis("Mouse X") * Vector3.up * Time.deltaTime * HeadRoateSpeed);
        
        if( Input.GetMouseButtonDown(0))
        {
            // 设置面板打开时，禁止开火
            if (SettingPanel.Instance.gameObject.activeSelf)
            {
                return;
            }
            // 退出面板打开时，禁止开火
            if (QuitPanel.Instance.gameObject.activeSelf)
            {
                return;
            }

            if (bulletNum >= 1)
            {
                Fire();
                bulletNum--;
                GamePanel.Instance.AddBullet(-1);
            }

        }

    }

    public override void Fire()
    {
        if(Time.deltaTime != 0)
        {
            if (nowWeapon != null)
            {
                nowWeapon.Fire();
            }
        }

    }

    public override void Dead()
    {
        Time.timeScale = 0;
        FailPanel.Instance.ShowMe();
    }

    public override void Hurt(TankBaseObj obj)
    {
        base.Hurt(obj);
        GamePanel.Instance.ChangeHP(this.MaxHp, this.Hp);
    }

    //切换武器
    public void ChangeWeapon(GameObject weaponObj)
    {
        if( weaponObj != null )
        {
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }

        GameObject gameObj = Instantiate(weaponObj, weaponPos, false);
        nowWeapon = gameObj.GetComponent<WeaponObj>();
        nowWeapon.SetFather(this);

    }

    //改变属性
    public void ChangeStat(E_Stat e_Stat, int index)
    {
        switch (e_Stat) 
        {
            case E_Stat.Hp:
                if(this.Hp + index > this.MaxHp)
                {
                    this.Hp = this.MaxHp;
                    GamePanel.Instance.ChangeHP(this.MaxHp, this.Hp);
                }
                else
                {
                    this.Hp += index;
                    GamePanel.Instance.ChangeHP(this.MaxHp, this.Hp);
                }
                    break;
            case E_Stat.Def:
                this.def += index;

                break;
            case E_Stat.Atk:
                this.atk += index;
                break;
            case E_Stat.Star:
                GamePanel.Instance.AddScore(10);
                bulletSuiPian += 1;
                if(bulletSuiPian >= 7)
                {
                    bulletNum++;
                    bulletSuiPian = 0;
                    GamePanel.Instance.AddBullet(1);
                }
                break;
        }

    }
}
