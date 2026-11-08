using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    public CustomGUILabel LabScore;
    public CustomGUILabel LabTime;

    public CustomGUILabel LabBullet;
    private int bulletNum = 0;


    public CustomGUIButten BtnSetting;
    public CustomGUIButten BtnQuit;

    public CustomGUITexture TexHp;

    //纪录分数和时间
    [HideInInspector]
    public int NowScore = 0;
    [HideInInspector]
    public float NowTime;
    private int time;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        BtnSetting.action += () =>
        {
            //点击设置图片触发

        };

        BtnQuit.action += () =>
        {
            //点击退出图标触发
            QuitPanel.Instance.ShowMe();
            SettingPanel.Instance.HideMe();
            Time.timeScale = 0;
        };
    }
    void Update()
    {
        #region 时间相关函数

        NowTime += Time.deltaTime;
        time = (int)NowTime;
        LabTime.content.text = "";
        if (time / 3600 > 0)
        {
            LabTime.content.text += time / 3600 + "时";
        }
        if (time % 3600 / 60 > 0)
        {
            LabTime.content.text += time % 3600 / 60 + "分";
        }
        LabTime.content.text += time % 60 + "秒";

        #endregion

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingPanel.Instance.ShowMe();
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
        }

    }

    public void AddScore(int score)
    {
        NowScore += score;
        LabScore.content.text = NowScore.ToString();
    }

    public void ChangeHP(int MaxHP, int HP)
    {
        TexHp.pos.width = (float)HP / MaxHP * 350;
    }

    public void AddBullet(int index)
    {
        bulletNum += index;
        LabBullet.content.text = bulletNum.ToString();
    }


}
