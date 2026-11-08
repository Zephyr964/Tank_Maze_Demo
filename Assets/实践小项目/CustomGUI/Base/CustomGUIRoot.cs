using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    public CustomGUIControl[] customGUIControls;
    void Start()
    {
        customGUIControls = GetComponentsInChildren<CustomGUIControl>(includeInactive:false);
    }

    private void OnGUI()
    {
        if (!Application.isPlaying)
        {
            customGUIControls = GetComponentsInChildren<CustomGUIControl>();
        }
        else
        {
            customGUIControls = GetComponentsInChildren<CustomGUIControl>(includeInactive: false);
        }

            for (int i = 0; i < customGUIControls.Length; i++)
            {
                customGUIControls[i].DrawGUI();
            }

    }
}
