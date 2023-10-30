using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(-98)]
public class chickenManager : MonoBehaviour
{
    [Tooltip("the chicken prefab used for instantiating the main chicken")]
    public GameObject mamaChicken;
    public GameObject npcChicken;
    public float loopDuration;

    private Transform[] path;
    private List<GameObject> chickens = new List<GameObject>();
    private enum Warp { waiting, warping, complete };
    private Warp timeWarp;
    private int framesWarped = 0;


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
                                               "time", loopDuration,
                                               "path", path,
                                               "looktime", 0.1,
                                               "lookahead", 0.07,
                                               "easetype", iTween.EaseType.linear,
                                               "looptype", iTween.LoopType.loop,
                                               "orienttopath", true,
                                               "delay", 20.0f));
        
        Time.fixedDeltaTime = 0.02f;
        timeWarp = Warp.waiting;
    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        if (framesWarped == 0)
        {
            Time.timeScale = 10.0f;
        }
        else if (framesWarped == loopDuration * 50f / 10f)
        {
            Time.timeScale = 1.0f;
            Debug.Log(Time.time);
        }
        framesWarped++;
        
#else
        if (timeWarp == Warp.waiting)
        {
            Time.timeScale = loopDuration / 0.02f;
            timeWarp = Warp.warping;
        } else if (timeWarp == Warp.warping)
        {
            timeWarp = Warp.complete;
        }
#endif
        }
}
