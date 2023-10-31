using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxDynamics : MonoBehaviour
{
    [Range(0, 1)] public float skyBoxInterp;
    public Material skyBoxMat;
    private float lastInterp;
    // Start is called before the first frame update
    void Start()
    {
        skyBoxInterp = 0;
        RenderSettings.ambientIntensity = skyBoxInterp;
        RenderSettings.reflectionIntensity = skyBoxInterp/2;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(lastInterp != skyBoxInterp) {
            skyBoxMat.SetFloat("_InterpAmount", skyBoxInterp);
            RenderSettings.ambientIntensity = skyBoxInterp;
            RenderSettings.reflectionIntensity = skyBoxInterp/2;
        }
        lastInterp = skyBoxInterp;
    }
}
