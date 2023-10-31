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

    private class Chicken
    {
        public GameObject gameObject;
        public float percentage;
        public GameObject lookTarget;
    }

    private Transform[] path;
    private Chicken mainChicken;
    private List<Chicken> npcChickenList = new List<Chicken>();

    private int fixedFrameCount = 0;

    void Start()
    {
        Time.fixedDeltaTime = 0.02f;

        path = GetComponent<pathManager>().animationPath;
        Debug.Log("Animation Path Length: " + path.Length);

        mainChicken = new Chicken {
                      gameObject = Instantiate(mainChicknePrefab, path[0].position, Quaternion.identity),
                      percentage = 0f,
                      lookTarget = null };

        
        for (int i = 0; i < numNPCChicken; i++)
        {
            float percent = 1f - (1f / (numNPCChicken + 1) * (i + 1));

            npcChickenList.Add(new Chicken {
                               gameObject = Instantiate(npcChickenPrefab, iTween.PointOnPath(path, percent), Quaternion.identity),
                               percentage = percent,
                               lookTarget = i == 0 ? mainChicken.gameObject : npcChickenList[i - 1].gameObject });
        }

        mainChicken.lookTarget = npcChickenList[numNPCChicken - 1].gameObject;
    }

    private void Update()
    {
        float percentageDelta = Time.deltaTime / loopDuration;

        mainChicken.percentage = mainChicken.percentage >= 1f ? 0f : mainChicken.percentage + percentageDelta;
        mainChicken.gameObject.transform.position = iTween.PointOnPath(path, mainChicken.percentage);

        foreach (Chicken chicken in npcChickenList) 
        {
            chicken.percentage = chicken.percentage >= 1f ? 0f : chicken.percentage + percentageDelta;
            chicken.gameObject.transform.position = iTween.PointOnPath(path, chicken.percentage);
        }
    }
    void FixedUpdate()
    {
        iTween.LookUpdate(mainChicken.gameObject, iTween.Hash(//"axis", "y",
                                                            "time", 0.02f,
                                                            "looktarget", mainChicken.lookTarget.transform));

        if (fixedFrameCount % optimizationFactor == 0)
        {
            foreach (Chicken chosenOne in  npcChickenList)
                {
                    iTween.LookUpdate(chosenOne.gameObject, iTween.Hash(//"axis", "y",
                                                                        "time", 0.02f * npcChickenList.Count(),
                                                                        "looktarget", chosenOne.lookTarget.transform));
                }
        }
        
        fixedFrameCount++;
    }
}
