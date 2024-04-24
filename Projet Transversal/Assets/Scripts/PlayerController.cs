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
    public LayerMask whatIsGarbage;
    public float garbageCheckRadius;
    public float parryCheckRadius = 0.5f;
    public GameObject garbage;
    public GameObject bin;
    public Transform throwPoint;
    public bool isInRange;

    //public AudioSource shootSound;
    //public AudioSource hitSound;
    //public AudioSource grabSound;
    //public AudioSource parrySound;
    
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

        if(Input.GetKeyDown(throwGarbage) && nbrTrash != 0 && !isInRange)
        {
            Vector2 throwDirection = (throwPoint.position - transform.position).normalized;
            GameObject garbageClone = (GameObject)Instantiate(garbage, throwPoint.position, Quaternion.identity);
            nbrTrash--;
            garbageClone.GetComponent<Rigidbody2D>().velocity = throwDirection * 15.5f;
            CinemachineShake.Instance.ShakeCamera(0.3f, 0.1f);
            //anim.SetTrigger("Throw");
            //shootSound.Play();
        }

        //Collider2D[] garbageToParry = Physics2D.OverlapCircleAll(transform.position, parryCheckRadius, whatIsGarbage);
        //foreach (Collider2D garbage in garbageToParry)
        //{
        //    if (Input.GetKeyDown(throwGarbage) && garbage.gameObject != throwPoint.gameObject)
        //    {
        //        garbage.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        //        Vector2 parryDirection = (bin.transform.position - transform.position).normalized;
        //        CinemachineShake.Instance.ShakeCamera(0.75f, 0.15f);
        //        garbage.GetComponent<Rigidbody2D>().velocity = parryDirection * 15f;
        //    }
        //}

        if (Input.GetKeyDown(recupTrash) && isInRange)
        {
            nbrTrash++;
            Debug.Log(nbrTrash);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Garbage")
        {
            CinemachineShake.Instance.ShakeCamera(1.5f, 0.25f);
            Destroy(coll.gameObject);
            Debug.Log("Hit!!!");
        }
    }

    //void HandleSpriteFlip()
    //{
    //    if (direction.x < 0) {
    //        gameObject.transform.rotation = Quaternion.Euler(0,180,0);
    //    } else if (direction.x > 0) {
    //        gameObject.transform.rotation = Quaternion.Euler(0, 0 ,0);
    //    }
    //}
}
