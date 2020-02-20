using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        this.transform.LookAt(new Vector3(0f, 0f, 0f));
        
    }

  
}
