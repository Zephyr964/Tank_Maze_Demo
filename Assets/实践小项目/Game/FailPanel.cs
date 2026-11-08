using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailPanel : BasePanel<FailPanel>
{
    public CustomGUIButten btnQuit;
    public CustomGUIButten btnReturn;
    void Start()
    {
        btnQuit.action += () =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("BeginScene");
        };
        btnReturn.action += () =>
        {
            SceneManager.LoadScene("GameScene");
        };
        HideMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
