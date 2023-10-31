using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassScalar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject grassObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform grassChild in grassObj.transform) {
            float delta = ((grassChild.position.x % 1.7f) + (grassChild.position.y % 3.9f))/5.6f;
        }
    }
}
