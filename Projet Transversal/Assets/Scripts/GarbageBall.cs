using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBall : MonoBehaviour
{
private Rigidbody2D theRB;

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }    
}
