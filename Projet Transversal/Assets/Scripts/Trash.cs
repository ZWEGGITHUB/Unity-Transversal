using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public Sprite[] sprites;
    private float maxRotationAngle = 20f;

    void Start()
    {
        int randomIndex = Random.Range(0, sprites.Length);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[randomIndex];

            float randomRotation = Random.Range(-maxRotationAngle, maxRotationAngle);
            transform.Rotate(Vector3.forward, randomRotation);
        }
    }

    void Update()
    {
        
    }
}
