using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel<QuitPanel>
{
    public CustomGUIButten BtnQuit;
    public CustomGUIButten BtnReturn;
    public CustomGUIButten settingPanel;

    void Start()
    {
        BtnQuit.action += () =>
        {
            SceneManager.LoadScene("BeginScene");
        };

        BtnReturn.action += () => 
        {
            HideMe();
            Cursor.lockState = CursorLockMode.Locked;
        };

        settingPanel.action += () =>
        {
            HideMe();
            SettingPanel.Instance.ShowMe();
            Time.timeScale = 0;
        };
        HideMe();
    }

    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }

    void Update()
    {
        
    }
}
