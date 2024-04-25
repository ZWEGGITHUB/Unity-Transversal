using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public bool isInPlayerRange1;
    public bool isInPlayerRange2;
    public LayerMask whatIsPlayer1;
    public LayerMask whatIsPlayer2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //isInPlayerRange1 = Physics2D.OverlapCircle(transform.position, 0.5f, whatIsPlayer1);
        //isInPlayerRange2 = Physics2D.OverlapCircle(transform.position, 0.5f, whatIsPlayer2);
    }
    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(transform.position, 0.7f);
        Gizmos.DrawWireSphere(transform.position, 1.25f);
    }
}
