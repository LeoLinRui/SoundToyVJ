using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-99)]
public class pathManager : MonoBehaviour
{
    public GameObject[] modules;

    [System.NonSerialized]
    public Transform[] animationPath;

    private List<Transform> pathList = new List<Transform>();
    private uint gizmoDrawCount = 0;

    void Awake()
    {
        Collect();
    }

    
    private void _OnDrawGizmos()
    {
        if (gizmoDrawCount == 30)
        {
            gizmoDrawCount = 0;
            Collect();
        } else
        {
            gizmoDrawCount++;
        }
        
        iTween.DrawPath(animationPath);
    }

    private void Collect()
    {
        foreach (var module in modules)
        {
            pathList.AddRange(module.transform.Find("waypoints").gameObject.GetComponent<waypoints>().path);
        }

        animationPath = pathList.ToArray();
    }

    private void _PrintAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Debug.Log(child.name);
            _PrintAllChildren(child.gameObject);
        }
    }
}
