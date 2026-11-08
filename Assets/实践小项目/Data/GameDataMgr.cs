using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance
    {
        get { return instance; }
    }

    public MusicData musicData;
    public RankList rankData;

    private GameDataMgr()
    {
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "music") as MusicData;
        if (!musicData.MusicIsFirst)
        {
            musicData.MusicIsFirst = true;
            musicData.XiaoSize = 1;
            musicData.YueSize = 1;
            musicData.YinXiao = true;
            musicData.YinYue = true;
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
        }

        rankData = PlayerPrefsDataMgr.Instance.LoadData(typeof(RankList), "rank") as RankList;
      

    }
    #region 写一下简单的api控制设置界面
    public void SetYinYue(bool yinYue)
    {
        musicData.YinYue = yinYue;
        BKmusic.Instance.ChangeOpen(yinYue);

        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
    public void SetYinXiao(bool yinXiao)
    {
        musicData.YinXiao = yinXiao;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
    public void SetYueSize(float yueSize)
    {
        musicData.YueSize=yueSize;

        BKmusic.Instance.ChangeValue(yueSize);
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
    public void SetXIaoSize(float XiaoSize)
    {
        musicData.XiaoSize = XiaoSize;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
    #endregion

    #region 写一下简单的api控制排行榜
    public void AddRankInfo(int score, string name, float time)
    {
        rankData.list.Add(new RankInfo(score, name, time));
        rankData.list.Sort((a, b) => a.time < b.time ? -1 : 1);
        for(int i = rankData.list.Count; i >= 10; i--)
        {
            rankData.list.RemoveAt(i);
        }
        PlayerPrefsDataMgr.Instance.SaveData(rankData, "rank");
    }

    #endregion


}
