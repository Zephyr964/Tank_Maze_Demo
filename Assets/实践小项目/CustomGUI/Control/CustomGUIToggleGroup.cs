using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CustomGUIToggleGroup : MonoBehaviour
{
    public CustomGUIToggle[] customGUIToggles;
    private bool oldToggleBool;
    void Start()
    {
        if (customGUIToggles.Length == 0)
            return;

        for(int i = 0; i < customGUIToggles.Length; i++)
        {
            CustomGUIToggle toggle = customGUIToggles[i];
            toggle.action += (value) =>
            {           
                if (value)
                {
                    for(int j = 0; j < customGUIToggles.Length; j++)
                    {
                        if (customGUIToggles[j] != toggle)
                        {
                            customGUIToggles[j].isBool = false;
                        }
                    }
                    oldToggleBool = toggle;
                }
                else if(value == oldToggleBool)
                {
                    toggle.isBool = true;
                }

            };
        }
    }

}
