using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class waypoints : MonoBehaviour
{
    public uint numWaypoints = 6;
    
    [System.NonSerialized]
    public List<Transform> path = new List<Transform>();

    void Awake()
    {
        for (int i = 1; i <= numWaypoints; i++)
        {
            Transform childTransform = this.transform.Find(i.ToString()).transform;
            if (childTransform != null)
            {
                path.Add(childTransform.transform);
            }
        }
    }
}
