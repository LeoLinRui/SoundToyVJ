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
        
        chickens.Add(Instantiate(mamaChicken, path[0]));
        iTween.MoveTo(chickens[0], iTween.Hash("name", "chickenAnimation", 
                                               "time", 30.0f, 
                                               "path", path, 
                                               "looktime", 1, 
                                               "lookahead", 0.6,
                                               "easetype", iTween.EaseType.linear,
                                               "looptype", iTween.LoopType.loop,
                                               "orienttopath", true));
    }
}
