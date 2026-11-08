using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Stat 
{
    Hp,
    Def,
    Atk,
    Star
}



public class RewardStats : MonoBehaviour
{
    public E_Stat e_Stat;
    public int StatSize;
    public GameObject audioSourceEff;
    void Start()
    {
        
    }

    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //改变属性
            PlayerObj obj = other.GetComponent<PlayerObj>();
            obj.ChangeStat(e_Stat, StatSize);

            //播放特效
            GameObject obj2 = Instantiate(audioSourceEff, this.transform.position, this.transform.rotation);
            AudioSource audioSource = obj2.GetComponent<AudioSource>();
            audioSource.volume = GameDataMgr.Instance.musicData.XiaoSize;
            audioSource.mute = !GameDataMgr.Instance.musicData.YinXiao;



            //碰撞消除自己
            Destroy(this.gameObject);
        }

    }
}
