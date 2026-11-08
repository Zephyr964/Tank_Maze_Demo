using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//排行榜单个信息
public class RankInfo
{
    public int score;
    public string name;
    public float time;

    public RankInfo()
    {

    }

    public RankInfo(int score, string name, float time)
    {
        this.score = score;
        this.name = name;
        this.time = time;
    }
}

//排行榜所有信息数组
public class RankList 
{
    public List<RankInfo> list;
}
