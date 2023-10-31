using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoopBehaviorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log("here");
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("LOD") || other.gameObject.CompareTag("NPC"))  {
            foreach(Renderer renderer in other.transform.parent.GetComponentsInChildren<Renderer>()) {
                renderer.enabled = false;
            }
        } if(other.gameObject.CompareTag("Player")) {
            Transform poopT = other.transform.parent.Find("poopChicken");
            foreach(Renderer renderer in poopT.GetComponentsInChildren<Renderer>()) {
                renderer.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            Transform poopT = other.transform.parent.Find("poopChicken");
            poopT.parent = other.transform.parent.parent;
            poopT.AddComponent<Rigidbody>();
            poopT.GetComponent<Rigidbody>().useGravity = true;
            poopT.GetComponent<Rigidbody>().drag = 10f;
            poopT.GetComponent<Collider>().enabled = true;
        }
    }
}
