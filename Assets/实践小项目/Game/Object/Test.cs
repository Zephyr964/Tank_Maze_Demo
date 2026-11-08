using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public MImi mImi;
    public int i;
    public int j;
    void Start()
    {
        if (mImi.grids[i,j].up)
        {
            print("ио");
        }
        if (mImi.grids[i, j].down)
        {
            print("об");
        }
        if (mImi.grids[i, j].right)
        {
            print("ср");
        }
        if (mImi.grids[i, j].left)
        {
            print("вС");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
