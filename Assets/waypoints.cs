using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoints : MonoBehaviour
{
    public uint numWaypoints = 6;
    
    [System.NonSerialized]
    public List<Transform> waypointTransforms = new List<Transform>();

    private void Awake()
    {
        for (int i = 1; i <= numWaypoints; i++)
        {
            Transform childTransform = this.transform.Find(i.ToString());
            if (childTransform != null)
            {
                waypointTransforms.Add(childTransform);
            }
        }
    }
}
