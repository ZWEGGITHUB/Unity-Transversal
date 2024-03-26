using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 direction;
    public float moveSpeed;
    public float jumpForce;

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;
    public KeyCode one;
    public KeyCode throwGarbage;

    private Rigidbody2D theRB;
    public Transform garbageCheckPoint;
    public LayerMask whatIsGarbage;
    public float garbageCheckRadius;
    public SpriteRenderer spriteRenderer;
    public GameObject player;
    private Animator anim;

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

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        theRB.velocity = direction * moveSpeed;

        HandleSpriteFlip();

        if(Input.GetKeyDown(throwGarbage))
        {
            //GameObject garbageClone = (GameObject)Instantiate(garbage, throwPoint.position, throwPoint.rotation);
            //garbageClone.transform.localScale = transform.localScale;       
            //anim.SetTrigger("Throw");
            shootSound.Play();
        }

        //anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
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
