using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireColliderBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Material defeatheredMat;
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
            body.GetComponent<Renderer>().material = defeatheredMat;
            GameObject armature = other.transform.parent.Find("Armature").gameObject;
            //Debug.Log(armature.GetComponentsInChildren<Transform>().Length);
            foreach(Renderer renderer in armature.GetComponentsInChildren<Renderer>()) {
                renderer.enabled = false;
            }
            if(other.gameObject.CompareTag("Player")) {
                Debug.Log("here");
                ParticleSystem explosion = other.transform.parent.parent.Find("Feather Explosion").GetComponent<ParticleSystem>();
                explosion.Play();
            }
        }
        
    }
}
