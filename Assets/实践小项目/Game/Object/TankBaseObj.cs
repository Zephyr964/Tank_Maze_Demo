using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public abstract class TankBaseObj : MonoBehaviour
{
    //炮管子
    public GameObject tankHead;

    public int atk;
    public int def;
    public int MaxHp = 100;
    public int Hp = 100;
    public float MoveSpeed = 10;
    public float RotateSpeed = 100;
    public float HeadRoateSpeed = 100;

    //死亡特效
    public GameObject deadEff;

    //死亡标记
    [HideInInspector] public bool isDead = false;

    public abstract void Fire();

    public virtual void Dead()
    {
        if (isDead) return;
        isDead = true;

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }


        Destroy(this.gameObject, 0.05f);

        if (deadEff != null)
        {
            //实例化死亡音效对象
            GameObject effObj = Instantiate(deadEff, this.transform.position, this.transform.rotation);
            //实例化死亡音效
            AudioSource audioSource = effObj.GetComponent<AudioSource>();
            //设置音效大小和开关
            audioSource.volume = GameDataMgr.Instance.musicData.XiaoSize;
            audioSource.mute = GameDataMgr.Instance.musicData.YinXiao;
            audioSource.Play();

        }

    }

    public virtual void Hurt(TankBaseObj obj)
    {
        if (isDead) return;

        if (this.def > obj.atk)
        {
            return;
        }
        this.Hp -= obj.atk - this.def;
        if (this.Hp <= 0)
        {
            this.Hp = 0;
            this.Dead();
        }
    }
}