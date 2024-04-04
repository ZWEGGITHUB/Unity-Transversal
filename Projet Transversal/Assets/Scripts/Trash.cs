using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public KeyCode f;
    public bool isInPlayerRange;
    public LayerMask whatIsPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isInPlayerRange = Physics2D.OverlapCircle(transform.position, 1, whatIsPlayer);

        if (Input.GetKeyDown(f) && isInPlayerRange)
        {
            StartCoroutine(waitt());
        }
    }
    IEnumerator waitt()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
