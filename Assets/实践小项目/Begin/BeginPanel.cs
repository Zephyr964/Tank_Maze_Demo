using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginPanel : BasePanel<BeginPanel>
{
    public CustomGUIButten BtnBegin;
    public CustomGUIButten BtnSetting;
    public CustomGUIButten BtnEnd;
    public CustomGUIButten BtnRank;

        
    void Start()
    {
        BtnBegin.action += () =>
        {
            SceneManager.LoadScene("GameScene");
        };
        BtnSetting.action += () =>
        {
            HideMe();
            SettingPanel.Instance.ShowMe();
        };
        BtnEnd.action += () =>
        {
            Application.Quit();
        };
        BtnRank.action += () =>
        {
            HideMe();
            RankPanel.Instance.ShowMe();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
