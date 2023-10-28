using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-99)]
public class pathManager : MonoBehaviour
{
    public GameObject[] modules;

    [System.NonSerialized]
    public List<Transform> animationPath = new List<Transform>();

    void Awake()
    {
        foreach (var module in modules)
        {
            animationPath.AddRange(module.transform.Find("waypoints").gameObject.GetComponent<waypoints>().path);
        }
    }

    void _PrintAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Debug.Log(child.name);
            _PrintAllChildren(child.gameObject);
        }
    }
}
