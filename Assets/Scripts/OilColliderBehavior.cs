using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilColliderBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Material cookedMat;
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
            body.GetComponent<Renderer>().material = cookedMat;
            other.transform.parent.parent.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
