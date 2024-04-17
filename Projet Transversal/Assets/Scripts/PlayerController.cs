using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.VisualScripting;
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
    public KeyCode recupTrash;
    public int nbrTrash = 0;

    private Rigidbody2D theRB;
    public LayerMask whatIsTrash;
    public float garbageCheckRadius;
    public SpriteRenderer spriteRenderer;
    public GameObject player;
    public GameObject garbage;
    public Transform throwPoint;

    public AudioSource shootSound;
    public AudioSource hitSound;
    public AudioSource grabSound;

    public bool isInRange;
    
    //public GameObject stunEffect;
    //public GameManager gameManager;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        isInRange = Physics2D.OverlapCircle(transform.position, garbageCheckRadius, whatIsTrash);

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

        if(Input.GetKeyDown(throwGarbage) && nbrTrash != 0 && !isInRange)
        {
            GameObject garbageClone = (GameObject)Instantiate(garbage, throwPoint.position, throwPoint.rotation);
            //garbageClone.transform.localScale = transform.localScale;
            nbrTrash--;
            //anim.SetTrigger("Throw");
            //shootSound.Play();
        }
        //CinemachineShake.Instance.ShakeCamera(2f, 0.2f);

        if (Input.GetKeyDown(recupTrash) && isInRange)
        {
            nbrTrash++;
            Debug.Log(nbrTrash);
            //Object.Destroy(GameObject.FindGameObjectWithTag("Trash"));
        }
    }

    void HandleSpriteFlip()
    {
        if (direction.x < 0) {
            gameObject.transform.rotation = Quaternion.Euler(0,180,0);
        } else if (direction.x > 0) {
            gameObject.transform.rotation = Quaternion.Euler(0, 0 ,0);
        }
    }
}
