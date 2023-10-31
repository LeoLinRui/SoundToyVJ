using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(-99)]
public class chickenManager : MonoBehaviour
{
    [Tooltip("the chicken prefab used for instantiating the main chicken")]
    public GameObject mainChicknePrefab;
    public GameObject npcChickenPrefab;
    public float loopDuration;
    public int numNPCChicken;
    public int optimizationFactor = 1;

    [System.NonSerialized]
    public GameObject mainChickenObject;

    private class Chicken
    {
        public GameObject gameObject;
        public float percentage;
        public Chicken lookTarget;
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

        // mainChickenObject exported for other scripts
        mainChickenObject = Instantiate(mainChicknePrefab, path[0].position, Quaternion.identity);

        mainChicken = new Chicken {
                      gameObject = mainChickenObject,
                      percentage = 0f,
                      lookTarget = null };
        
        for (int i = 0; i < numNPCChicken; i++)
        {
            float percent = 1f - (1f / (numNPCChicken + 1) * (i + 1));

            npcChickenList.Add(new Chicken {
                               gameObject = Instantiate(npcChickenPrefab, iTween.PointOnPath(path, percent), Quaternion.identity),
                               percentage = percent,
                               lookTarget = i == 0 ? mainChicken : npcChickenList[i - 1] });
        }

        mainChicken.lookTarget = npcChickenList[numNPCChicken - 1];
    }

    private void Update()
    {
        float percentageDelta = Time.deltaTime / loopDuration;

        updateChickenPos(mainChicken, percentageDelta, mainChicknePrefab);

        foreach (Chicken chicken in npcChickenList) 
        {
            updateChickenPos(chicken, percentageDelta, npcChickenPrefab);
        }
    }
    void FixedUpdate()
    {
        iTween.LookUpdate(mainChicken.gameObject, iTween.Hash(//"axis", "y",
                                                            "time", 0.02f,
                                                            "looktarget", mainChicken.lookTarget.gameObject.transform));

        if (fixedFrameCount % optimizationFactor == 0)
        {
            foreach (Chicken chosenOne in  npcChickenList)
                {
                    iTween.LookUpdate(chosenOne.gameObject, iTween.Hash(//"axis", "y",
                                                                        "time", 0.02f * npcChickenList.Count(),
                                                                        "looktarget", chosenOne.lookTarget.gameObject.transform));
                }
        }
        
        fixedFrameCount++;
    }

    private void updateChickenPos(Chicken chicken, float delta, GameObject prefab)
    {
        if (chicken.percentage + delta > 1f) // restart
        {
            Destroy(chicken.gameObject);
            chicken.percentage = chicken.percentage + delta - 1f;
            chicken.gameObject = Instantiate(prefab, iTween.PointOnPath(path, chicken.percentage), Quaternion.identity);
            
        } else
        {
            chicken.percentage = chicken.percentage >= 1f ? 0f : chicken.percentage + delta;
            chicken.gameObject.transform.position = iTween.PointOnPath(path, chicken.percentage);
        }
    }
}
