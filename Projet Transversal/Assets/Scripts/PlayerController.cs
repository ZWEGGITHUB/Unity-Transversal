using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TMP_Text trashText;
    private Rigidbody2D theRB;
    public LayerMask whatIsTrash;
    public LayerMask whatIsGarbage;
    public float garbageCheckRadius;
    public float parryCheckRadius = 0.6f;
    public GameObject garbage;
    public GameObject bin;
    public Transform throwPoint;
    public bool isInRange;
    public bool isStunt;

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

        if(Input.GetKeyDown(throwGarbage) && nbrTrash != 0 && !isInRange)
        {
            Vector2 throwDirection = (throwPoint.position - transform.position).normalized;
            GameObject garbageClone = (GameObject)Instantiate(garbage, throwPoint.position, Quaternion.identity);
            nbrTrash--;
            trashText.text = "Trash : " + nbrTrash;
            garbageClone.GetComponent<Rigidbody2D>().velocity = throwDirection * 15.5f;
            CinemachineShake.Instance.ShakeCamera(0.3f, 0.1f);
            //anim.SetTrigger("Throw");
            //shootSound.Play();
        }

        Vector3 poz = transform.position - new Vector3 (0f, 0.15f, 0f);
        Collider2D[] garbageToParry = Physics2D.OverlapCircleAll(poz, parryCheckRadius, whatIsGarbage);
        foreach (Collider2D garbage in garbageToParry)
        {
            if (Input.GetKeyDown(throwGarbage))
            {
                Vector2 parryDirection = (bin.transform.position - transform.position).normalized;
                CinemachineShake.Instance.ShakeCamera(0.8f, 0.3f);
                garbage.GetComponent<Rigidbody2D>().velocity = parryDirection * 15.5f;
            }
        }

        Collider2D[] isInRangeOfTrash = Physics2D.OverlapCircleAll(transform.position, garbageCheckRadius, whatIsTrash);
        foreach (Collider2D trash in isInRangeOfTrash)
        {
            if (Input.GetKeyDown(recupTrash))
            {
                nbrTrash++;
                trashText.text = "Trash : " + nbrTrash;
                Debug.Log(nbrTrash);
            }
        }

        //HandleSpriteFlip();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Garbage")
        {
            CinemachineShake.Instance.ShakeCamera(1.5f, 0.25f);
            Destroy(coll.gameObject);
            Debug.Log("Hit!!!");
            StartCoroutine(StuntDelay());
        }
    }
    IEnumerator StuntDelay()
    {
        isStunt = true;
        theRB.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.5f);
        isStunt = false;
    }
    void OnDrawGizmosSelected() 
    {
        Vector3 pos = transform.position - new Vector3 (0f, 0.15f, 0f);
        Gizmos.DrawWireSphere(pos, parryCheckRadius);
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
