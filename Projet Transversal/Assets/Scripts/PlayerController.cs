using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Vector2 direction;
    public float moveSpeed;
    private Animator animator;
    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;
    public KeyCode throwGarbage;
    public KeyCode recupTrash;
    public KeyCode parryKey;
    public int nbrTrash = 0;
    public TMP_Text trashText;
    private Rigidbody2D theRB;
    public LayerMask whatIsTrash;
    public LayerMask whatIsGarbage;
    public float garbageCheckRadius;
    public float parryCheckRadius = 0.6f;
    public GameObject garbage;
    public Transform bin;
    public Transform throwPoint;
    public bool isInRange;
    public bool isStunt;

    public ParticleSystem hitParticleSystem;

    //public AudioSource shootSound;
    //public AudioSource hitSound;
    //public AudioSource grabSound;
    //public AudioSource parrySound;
    
    //public GameObject stunEffect;
    //public GameManager gameManager;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        isInRange = Physics2D.OverlapCircle(transform.position, garbageCheckRadius, whatIsTrash);
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal") );
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
        //animator.SetFloat("Speed", direction.sqrMagnitude);
        if (isStunt == false) 
        {
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
        }

        if(Input.GetKeyDown(throwGarbage) && nbrTrash != 0 && !isInRange && isStunt == false)
        {
            animator.SetTrigger("IsShooting");
            Vector2 throwDirection = (throwPoint.position - transform.position).normalized;
            GameObject garbageClone = (GameObject)Instantiate(garbage, throwPoint.position, Quaternion.identity);
            nbrTrash--;
            trashText.text = "Trash : " + nbrTrash;
            garbageClone.GetComponent<Rigidbody2D>().velocity = throwDirection * 15.5f;
            CinemachineShake.Instance.ShakeCamera(0.3f, 0.1f);
            //shootSound.Play();
        }

        Vector3 poss = transform.position - new Vector3 (0f, 0.18f, 0f);
        Collider2D[] isInRangeOfTrash = Physics2D.OverlapCircleAll(poss, garbageCheckRadius, whatIsTrash);
        foreach (Collider2D trash in isInRangeOfTrash)
        {
            if (Input.GetKeyDown(recupTrash) && isStunt == false)
            {
                animator.SetTrigger("IsGrabing");
                nbrTrash++;
                trashText.text = "Trash : " + nbrTrash;
                Destroy(trash.gameObject);
            }
        }

        if (Input.GetKeyDown(parryKey))
        {
            Parry();
        }

        //HandleSpriteFlip();
    }
    private void Parry()
    {
        Vector3 parryPosition = transform.position - new Vector3(0f, 0.15f, 0f);
        Collider2D[] objectsToParry = Physics2D.OverlapCircleAll(parryPosition, parryCheckRadius, whatIsGarbage);

        foreach (Collider2D objCollider in objectsToParry)
        {
            Rigidbody2D objRigidbody = objCollider.GetComponent<Rigidbody2D>();
            if (objRigidbody != null)
            {
                animator.SetTrigger("IsParry");
                Vector2 parryDirection = (bin.position - transform.position).normalized;
                objRigidbody.velocity = parryDirection * 20f;
                CinemachineShake.Instance.ShakeCamera(1f, 0.3f);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Garbage")
        {
            CinemachineShake.Instance.ShakeCamera(1.5f, 0.25f);
            Destroy(coll.gameObject);
            StartCoroutine(StuntDelay());
        }
    }
    IEnumerator StuntDelay()
    {
        isStunt = true;
        animator.SetTrigger("IsStunt");
        Instantiate(hitParticleSystem, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        theRB.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.4f);
        isStunt = false;
    }
    void OnDrawGizmosSelected() 
    {
        Vector3 pos = transform.position - new Vector3 (0f, 0.15f, 0f);
        Gizmos.DrawWireSphere(pos, parryCheckRadius);
        Vector3 poss = transform.position - new Vector3 (0f, 0.18f, 0f);
        Gizmos.DrawWireSphere(poss, garbageCheckRadius);
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
