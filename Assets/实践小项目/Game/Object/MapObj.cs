using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObj : MonoBehaviour
{
    public GameObject Tank;
    private Vector3 Pos;
    public int Y;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Pos.x = Tank.transform.position.x;
        Pos.z = Tank.transform.position.z;
        Pos.y = Y;
        this.transform.position = Pos;
    }
}
