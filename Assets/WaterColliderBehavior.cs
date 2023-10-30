using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterColliderBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Material baseWetMat;
    public Material feather1WetMat; 
    public Material feather2WetMat; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("NPC")) {
            //chage materials of siblings
            GameObject body = transform.parent.Find("ChickenBody.001").gameObject;
            body.GetComponent<Renderer>().material = baseWetMat;
            GameObject armature = transform.parent.Find("ChickenBody.001").gameObject;

        }
    }
}
