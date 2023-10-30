using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class chickenManager : MonoBehaviour
{
    [Tooltip("the chicken prefab used for instantiating the main chicken")]
    public GameObject mainChicknePrefab;
    public GameObject npcChickenPrefab;
    public float loopDuration;
    public int numNPCChicken;
    public int optimizationFactor = 1;

    private class NPCChicken
    {
        public GameObject gameObject;
        public float percentage;
        public GameObject lookTarget;
    }

    private Transform[] path;
    private GameObject mainChicken;
    private List<NPCChicken> npcChickenList = new List<NPCChicken>();

    private int fixedFrameCount = 0;

    void Start()
    {
        Time.fixedDeltaTime = 0.02f;

        path = GetComponent<pathManager>().animationPath;
        Debug.Log("Animation Path Length: " + path.Length);

        mainChicken = Instantiate(mainChicknePrefab, path[0]);
        iTween.MoveTo(mainChicken, iTween.Hash("name", "mainChickenAnimation",
                                               "time", loopDuration,
                                               "path", path,
                                               "looktime", 0.1,
                                               "lookahead", 0.03,
                                               "easetype", iTween.EaseType.linear,
                                               "looptype", iTween.LoopType.loop,
                                               "orienttopath", true,
                                               "delay", 0f));
        
        for (int i = 0; i < numNPCChicken; i++)
        {
            float percent = 1f - (1f / (numNPCChicken + 1f) * (i + 1));

            npcChickenList.Add(new NPCChicken{
                               gameObject = Instantiate(npcChickenPrefab, iTween.PointOnPath(path, percent), Quaternion.identity),
                               percentage = percent,
                               lookTarget = i == 0 ? mainChicken : npcChickenList[i - 1].gameObject });
        }
    }

    private void Update()
    {
        float percentageDelta = Time.deltaTime / loopDuration;

        foreach (NPCChicken chicken in npcChickenList) 
        {
            chicken.percentage = chicken.percentage >= 1f ? 0f : chicken.percentage + percentageDelta;
            chicken.gameObject.transform.position = iTween.PointOnPath(path, chicken.percentage);
        }
    }
    void FixedUpdate()
    {
        // NPCChicken chosenOne = npcChickenList[fixedFrameCount % npcChickenList.Count()];
        if (fixedFrameCount % optimizationFactor == 0)
        {
            foreach (NPCChicken chosenOne in  npcChickenList)
                {
                    iTween.LookUpdate(chosenOne.gameObject, iTween.Hash("axis", "y",
                                                                        "time", 0.02f * npcChickenList.Count(),
                                                                        "looktarget", chosenOne.lookTarget.transform));
                }
        }
        
        fixedFrameCount++;
    }
}
