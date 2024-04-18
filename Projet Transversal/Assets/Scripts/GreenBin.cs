using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GreenBin : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trash trashPickup = collision.gameObject.GetComponent<Trash>();

        if (trashPickup != null)
        {
            score++;
            scoreText.text = "Score: " + score;
            Destroy(collision.gameObject);
        }
    }
}
