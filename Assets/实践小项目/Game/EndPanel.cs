using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanel : BasePanel<EndPanel>
{
    public CustomGUIButten btnQuit;
    public CustomGUIInput btnInput;


    void Start()
    {
        btnQuit.action += () =>
        {
            //这是回到游戏开始界面，现在这个界面出现时游戏已经暂停了所以回到开始界面要取消游戏暂停
            Time.timeScale = 1.0f;
            GameDataMgr.Instance.AddRankInfo(GamePanel.Instance.NowScore, this.btnInput.content.text, GamePanel.Instance.NowTime);
            SceneManager.LoadScene("BeginScene");
        };
        HideMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
