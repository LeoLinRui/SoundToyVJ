using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class pathManager : MonoBehaviour
{
    public GameObject[] modules;
    public GameObject wayPointVisualPrefab;

    [System.NonSerialized]
    public Transform[] animationPath;

    void Awake()
    {
        Collect();
    }

    private void OnDrawGizmos()
    {
        Collect();
        iTween.DrawPath(animationPath);

        foreach (Transform wayPoint in animationPath)
        {
            Gizmos.DrawSphere(wayPoint.position, 2f);
        }
    }

    private void Collect()
    {
        List<Transform> pathList = new List<Transform>();

        foreach (var module in modules)
        {
            Transform wayPointList = module.transform.Find("waypoints").transform;

            for (int i = 1; i <= wayPointList.childCount; i++)
            {
                Transform wayPoint = wayPointList.Find(i.ToString()).transform;

                if (wayPoint != null)
                {
                    pathList.Add(wayPoint.transform);
                }
            }
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
