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

    private uint gizmoDrawCount = 0;

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

    void _OnDrawGizmos()
    {
        if (gizmoDrawCount == 30)
        {
            gizmoDrawCount = 0;
            for (int i = 1; i <= numWaypoints; i++)
            {
                Transform childTransform = this.transform.Find(i.ToString()).transform;
                if (childTransform != null)
                {
                    path.Add(childTransform.transform);
                }
            }
        }
        else
        {
            gizmoDrawCount++;
        }
    }
}
