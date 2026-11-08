using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BKmusic : MonoBehaviour
{
    private  static BKmusic instance;
    public  static BKmusic Instance => instance;

    private AudioSource audioSource;
    void Start()
    {
        
    }

    private void Awake()
    {
        instance = this;
        this.audioSource = GetComponent<AudioSource>();

        ChangeOpen(GameDataMgr.Instance.musicData.YinYue);
        ChangeValue(GameDataMgr.Instance.musicData.YueSize);
    }

    public void ChangeOpen(bool IsOpen)
    {
        audioSource.mute = !IsOpen;
    }

    public void ChangeValue(float  value)
    {
        audioSource.volume = value;
    }
}
