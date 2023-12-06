using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationControl : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z >= 200 || transform.position.z <= -200)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z * -1);
        }
        if (transform.position.x >= 200 || transform.position.x <= -200)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
        }
    }
}
