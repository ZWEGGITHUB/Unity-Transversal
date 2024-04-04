using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterSetTime : MonoBehaviour
{
    public int sec = 3;
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(sec);
        Object.Destroy(this.gameObject);
    }


}
