using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    public float shootSpeed = 50;
    private TankBaseObj FatherObj;

    //子弹击中特效
    public GameObject eff;
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.Translate(Vector3.forward * shootSpeed * Time.deltaTime);
    }

    public void SetFather(TankBaseObj obj)
    {
        //告诉武器拥有者是谁
        FatherObj = obj;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == null)
        {
            Destroy(this.gameObject);
        }


        if (other.CompareTag("Cube") 
            || other.CompareTag("Player") && FatherObj.CompareTag("Monster")
            || other.CompareTag("Monster") && FatherObj.CompareTag("Player")
            || other.CompareTag("Player") && FatherObj.CompareTag("Tower")
            || other.CompareTag("Tower") && FatherObj.CompareTag("Player")

            )
        {
                //受伤逻辑处理
                TankBaseObj tankBaseObj = other.GetComponent<TankBaseObj>();
                if (tankBaseObj != null)
                {
                    tankBaseObj.Hurt(FatherObj);
                }


                if (eff != null)
                {
                    //击中爆炸特效
                    GameObject obj = Instantiate(eff, this.transform.position, this.transform.rotation);

                #region 淘汰的功能
                //if (FatherObj.CompareTag("Tower"))
                //{
                //    AudioSource audioSource1 = obj.GetComponent<AudioSource>();

                //    audioSource1.volume = 0;
                //    audioSource1.mute = false;
                //}
                //else
                //{
                //    AudioSource audioSource = obj.GetComponent<AudioSource>();

                //    audioSource.volume = GameDataMgr.Instance.musicData.XiaoSize;
                //    audioSource.mute = !GameDataMgr.Instance.musicData.YinXiao;
                //}
                #endregion



                }
                Destroy(this.gameObject);
            
        }



    }
}
