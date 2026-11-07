using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public CustomGUIButten rankQuit;
    List<CustomGUILabel> Names = new List<CustomGUILabel>();
    List<CustomGUILabel> Scores = new List<CustomGUILabel>();
    List<CustomGUILabel> Times = new List<CustomGUILabel>();

    private void Start()
    {
        //先通过代码找到对应的lab，把它们添加进List数组
        for(int i = 1; i <= 10; i++)
        {

            Names.Add(this.transform.Find("Name/LabName" + i).GetComponent<CustomGUILabel>());
            Scores.Add(this.transform.Find("Score/LabScore" + i).GetComponent<CustomGUILabel>());
            Times.Add(this.transform.Find("Time/LabTime" + i).GetComponent<CustomGUILabel>());
        }




        rankQuit.action += () =>
        {
            HideMe();
            BeginPanel.Instance.ShowMe();
        };
        HideMe ();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdatePanelInfo();
    }

    //跟新排行榜函数
    public void UpdatePanelInfo()
    {
        List<RankInfo> list = GameDataMgr.Instance.rankData.list;
        for(int i = 0; i < list.Count; i++)
        {
            Names[i].content.text = list[i].name;
            Scores[i].content.text = list[i].score.ToString();
            //设置时间 时，分，秒
            Times[i].content.text = "";
            int Time = (int)list[i].time;
            if(Time / 3600 > 0)
            {
                Times[i].content.text += Time / 3600 + "时";
            }
            if(Time % 3600 / 60 > 0)
            {
                Times[i].content.text += Time % 3600 / 60 + "分";
            }
            Times[i].content.text += Time % 60 + "秒";
        }
    }
}
