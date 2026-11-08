using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RewardWeapon : MonoBehaviour
{
    public GameObject[] weapons;

    public GameObject changeWeaponEff;
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
            int index = Random.Range(0, weapons.Length); 
            PlayerObj playerObj = other.GetComponent<PlayerObj>();
            playerObj.ChangeWeapon(weapons[index]);

            //≤•∑≈Ãÿ–ß
            GameObject obj = Instantiate(changeWeaponEff, this.transform.position, this.transform.rotation);
            AudioSource audioSource = obj.GetComponent<AudioSource>();

            audioSource.volume = GameDataMgr.Instance.musicData.XiaoSize;
            audioSource.mute = !GameDataMgr.Instance.musicData.YinXiao;

            Destroy(this.gameObject);
        }
    }
}
