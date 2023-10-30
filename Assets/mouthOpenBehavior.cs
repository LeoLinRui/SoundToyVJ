using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouthOpenBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shapeKeymesh;
    private bool isOpening = false;
    private const int MOUTH_OPEN_IDX = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SkinnedMeshRenderer renderer = shapeKeymesh.GetComponent<SkinnedMeshRenderer>();
        float originalWeight =  renderer.GetBlendShapeWeight(MOUTH_OPEN_IDX);
        if(isOpening) {
            renderer.SetBlendShapeWeight(MOUTH_OPEN_IDX, 
                    Mathf.Min(originalWeight + Time.deltaTime, 100.0f));
        } 
        else {
            renderer.SetBlendShapeWeight(MOUTH_OPEN_IDX, 
                    Mathf.Max(originalWeight - Time.deltaTime, 0.0f));
        }

    }

    void onTriggerEnter(Collider chicken) {
        isOpening = true;
    }
}
