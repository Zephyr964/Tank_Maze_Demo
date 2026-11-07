using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : BasePanel<SettingPanel>
{
    public CustomGUIToggle YueToggle;
    public CustomGUIToggle XiaoToggle;
    public CustomGUISilder YueSlider;
    public CustomGUISilder XiaoSlider;

    public CustomGUIButten SettingQuit;

    void Start()
    {
        YueToggle.action += (value) =>
        {
            GameDataMgr.Instance.SetYinYue(value);
        };
        XiaoToggle.action += (value) =>
        {
            GameDataMgr.Instance.SetYinXiao(value);
        };
        YueSlider.action += (value) =>
        {
            GameDataMgr.Instance.SetYueSize(value);
        };
        XiaoSlider.action += (value) =>
        {
            GameDataMgr.Instance.SetXIaoSize(value);
        };
        SettingQuit.action += () =>
        {
            HideMe();
            if(SceneManager.GetActiveScene().name == "GameScene")
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                BeginPanel.Instance.ShowMe();
            }

        };
        HideMe();

    }

    public void UpdateMusicData()
    {
        MusicData data = GameDataMgr.Instance.musicData;
        XiaoToggle.isBool = data.YinXiao;
        YueToggle.isBool = data.YinYue;
        XiaoSlider.nowIndex = data.XiaoSize;
        YueSlider.nowIndex = data.YueSize;
        PlayerPrefsDataMgr.Instance.SaveData(data, "music");
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdateMusicData();
    }

    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
