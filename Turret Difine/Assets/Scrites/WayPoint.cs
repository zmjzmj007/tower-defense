using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//得到路径点
public class WayPoint : MonoBehaviour {
    public static Transform[] positions;

    void Awake()
    {
        positions = new Transform[transform.childCount];
        for(int i=0;i<positions.Length;i++)
        {
            positions[i] = transform.GetChild(i);
        }
    }

}
