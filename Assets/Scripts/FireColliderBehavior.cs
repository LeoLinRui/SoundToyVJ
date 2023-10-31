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
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("LOD") || other.gameObject.CompareTag("NPC")) {
            //Debug.Log(other.gameObject.name);
            //chage materials of siblings
            GameObject body = other.transform.parent.Find("ChickenBody.001").gameObject;
            body.GetComponent<Renderer>().material = defeatheredMat;

            
            if (other.gameObject.CompareTag("Player")) {
                ParticleSystem explosion = other.transform.parent.Find("Feather Explosion").GetComponent<ParticleSystem>();
                ParticleSystem smoke = other.transform.parent.Find("Explosion Smoke").GetComponent<ParticleSystem>();
                explosion.Play();
                smoke.Play();

                GameObject armature = other.transform.parent.Find("Armature").gameObject;

                //Debug.Log(armature.GetComponentsInChildren<Transform>().Length);
                foreach(Renderer renderer in armature.GetComponentsInChildren<Renderer>()) {
                    renderer.enabled = false;
                }
            } else if (other.gameObject.CompareTag("NPC")) {
                ParticleSystem explosion = other.transform.parent.Find("Feather Explosion").GetComponent<ParticleSystem>();
                ParticleSystem smoke = other.transform.parent.Find("Explosion Smoke").GetComponent<ParticleSystem>();
                explosion.Play();
                smoke.Play();

                GameObject armature = other.transform.parent.Find("Armature").gameObject;

                //Debug.Log(armature.GetComponentsInChildren<Transform>().Length);
                foreach(Renderer renderer in armature.GetComponentsInChildren<Renderer>()) {
                    renderer.enabled = false;
                }
            } else //LOD
            {
                GameObject featherClump = other.transform.parent.Find("FeatherClump").gameObject;
                featherClump.GetComponent<Renderer>().enabled = false;
            }
        }
        
    }
}
