using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardWall : MonoBehaviour
{
    public GameObject[] rewardBox;
    public int wallHp;

    public GameObject DeadEff;
    void Start()
    {

    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            wallHp -= 1;
        }

        if(wallHp <= 0)
        {

            Destroy(this.gameObject);
            GameObject obj = Instantiate(DeadEff, this.transform.position, this.transform.rotation);
            AudioSource audioSource = obj.GetComponent<AudioSource>();

            audioSource.volume = GameDataMgr.Instance.musicData.XiaoSize;
            audioSource.mute = !GameDataMgr.Instance.musicData.YinXiao;


            //出现奖励属性的概率
            int index = Random.Range(0, 100);
            if (index < 99)
            {
                index = Random.Range(0, rewardBox.Length);
                Instantiate(rewardBox[index], this.transform.position, this.transform.rotation);
            }

        }
    }
}
