using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 direction;
    public float moveSpeed;

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;
    public KeyCode throwGarbage;

    private Rigidbody2D theRB;
    public Transform garbageCheckPoint;
    public LayerMask whatIsGarbage;
    public float garbageCheckRadius;
    public SpriteRenderer spriteRenderer;
    public GameObject player;

    //Creer une Liste de Garbage que a le joueur sur lui

    public GameObject garbage;
    public Transform throwPoint;

    public AudioSource shootSound;
    public AudioSource hitSound;
    public AudioSource grabSound;

    private Collider2D coll;

    public bool isShotgun;
    
    //public GameObject stunEffect;
    //public GameManager gameManager;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if(Input.GetKey(left))
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
        } else if(Input.GetKey(right))
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
        } else {
            theRB.velocity = new Vector2(0, theRB.velocity.y);
        }

        if(Input.GetKey(down))
        {
            theRB.velocity = new Vector2(theRB.velocity.x, -moveSpeed);
        } else if(Input.GetKey(up))
        {
            theRB.velocity = new Vector2(theRB.velocity.x, moveSpeed);
        } else {
            theRB.velocity = new Vector2(theRB.velocity.x, 0);
        }

        HandleSpriteFlip();

        if(Input.GetKeyDown(throwGarbage))
        {
            //GameObject garbageClone = (GameObject)Instantiate(garbage, throwPoint.position, throwPoint.rotation);
            //garbageClone.transform.localScale = transform.localScale;       
            //anim.SetTrigger("Throw");
            shootSound.Play();
        }
        
        //CinemachineShake.Instance.ShakeCamera(2f, 0.2f);
    }

    void HandleSpriteFlip()
    {
        if (!spriteRenderer.flipX && direction.x < 0) {
            spriteRenderer.flipX = true;
        } else if (spriteRenderer.flipX && direction.x > 0) {
            spriteRenderer.flipX = false;
        }
    }
}
