using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GreenBin : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;
    public ParticleSystem scoreParticleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Garbage")
        {
            score++;
            scoreText.text = "Score : " + score;
            Instantiate(scoreParticleSystem, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            CinemachineShake.Instance.ShakeCamera(1.5f, 0.25f);
            Destroy(collision.gameObject);
        }
    }
}
