using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(-98)]
public class chickenManager : MonoBehaviour
{
    [Tooltip("the chicken prefab used for instantiating chickens")]
    public GameObject mamaChicken;

    private Transform[] path;
    private List<GameObject> chickens = new List<GameObject>();

    private void Awake()
    {
        path = GetComponent<pathManager>().animationPath;
        Debug.Log("AnimationPath: " + path.Length);
    }

    void Start()
    {
        GameObject chickenInstance = Instantiate(mamaChicken, path[0]);
        chickens.Add(chickenInstance);
        //iTween.PutOnPath(chickenInstance, path, 0.5f);
        iTween.MoveTo(chickenInstance, iTween.Hash("name", "chickenAnimation", 
                                               "time", 90.0f, 
                                               "path", path, 
                                               "looktime", 0.1, 
                                               "lookahead", 0.07,
                                               "easetype", iTween.EaseType.linear,
                                               "looptype", iTween.LoopType.loop,
                                               "orienttopath", true,
                                               "delay", 60.0f));

    }
}
