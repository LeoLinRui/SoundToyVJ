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
            //Debug.Log(other.gameObject.name);
            //chage materials of siblings
            GameObject body = other.transform.parent.Find("ChickenBody.001").gameObject;
            body.GetComponent<Renderer>().material = baseWetMat;
            if (other.gameObject.CompareTag("Player")) {
                GameObject armature = other.transform.parent.Find("Armature").gameObject;
                //Debug.Log(armature.GetComponentsInChildren<Transform>().Length);
                foreach(Renderer renderer in armature.GetComponentsInChildren<Renderer>()) {
                    renderer.material = feather1WetMat;
                }
            } else {
                 GameObject feathers = other.transform.parent.Find("FeatherClump").gameObject;
                 feathers.GetComponent<Renderer>().material = feather1WetMat;
            }

        }
    }
}
