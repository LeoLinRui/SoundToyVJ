using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EatingColliderBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tubeMesh;
    public GameObject chairMesh;
    private Collider[] colliders;

    void Start()
    {
        colliders = this.GetComponents<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        bool inBaby = false;
        foreach(Collider col in colliders) {
            if (col.bounds.Contains(Camera.main.transform.position)) {
                inBaby = true;
                break;
            }
        }
        if (inBaby) {
            if(!tubeMesh.GetComponent<Renderer>().enabled) tubeMesh.GetComponent<Renderer>().enabled = true;
            if(chairMesh.GetComponent<Renderer>().enabled) chairMesh.GetComponent<Renderer>().enabled = false;
        } else {
           if(tubeMesh.GetComponent<Renderer>().enabled) tubeMesh.GetComponent<Renderer>().enabled = false;
           if(!chairMesh.GetComponent<Renderer>().enabled) chairMesh.GetComponent<Renderer>().enabled = true; 
        }
    }

    void onTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("NPC")) {
            return;
            //set mouth to close
        };
    }
}
