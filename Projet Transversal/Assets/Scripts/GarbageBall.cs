using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBall : MonoBehaviour
{
    //private Rigidbody2D ballRB;
    public Sprite[] sprites;

    void Start()
    {
        int randomIndex = Random.Range(0, sprites.Length);
        
        //ballRB = GetComponent<Rigidbody2D>();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[randomIndex];

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 2f);
    }    
}
