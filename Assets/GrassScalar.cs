using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassScalar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject grassObj;
    [Range(0, 1)]float speed = 0.1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform grassChild in grassObj.transform) {
            float delta = ((grassChild.position.x % 0.31f) + (grassChild.position.y % 0.69f) + Time.time*speed) % 1.0f;
            grassChild.localScale =  new Vector3(grassChild.localScale.x, delta*20f, grassChild.localScale.z);
        }
    }
}
