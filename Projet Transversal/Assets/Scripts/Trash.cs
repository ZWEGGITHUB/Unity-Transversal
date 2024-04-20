using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public KeyCode P1;
    public KeyCode P2;
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
        isInPlayerRange1 = Physics2D.OverlapCircle(transform.position, 0.5f, whatIsPlayer1);
        isInPlayerRange2 = Physics2D.OverlapCircle(transform.position, 0.5f, whatIsPlayer2);

        if (Input.GetKeyDown(P1) && isInPlayerRange1)
        {
            StartCoroutine(waitt());
        }
        if (Input.GetKeyDown(P2) && isInPlayerRange2)
        {
            StartCoroutine(waitt());
        }
    }
    IEnumerator waitt()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
