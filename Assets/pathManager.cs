using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pathManager : MonoBehaviour
{
    public GameObject[] modules;

    [System.NonSerialized]
    public GameObject[] animationPath;

    void Awake()
    {
        foreach (var module in modules)
        {
            animationPath.AddRange(module.transform.Find("waypoints").GetComponent<waypoints>().path);
        }
    }
}
