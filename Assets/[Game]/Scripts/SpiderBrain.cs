using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpiderBrain : MonoBehaviour
{
    public Transform rayPoint;
    private Transform _head;
    public float groundCheckDistance;
    public LayerMask groundLayer;
    public float yOffest;
    public Transform[] legTargets;
    public float speed;
    public float currspeed;
    public float vertspeed;
    public float offsetTimer;
    bool decrease;
    [Range(0f, 0.5f)]
    public float breatheHeight;
    public float breatheSpeed;
    public LegMover[] legs;
    Rigidbody2D RB;
    bool canJump;
    public bool canClimb;
    public bool isRightClimb;
    public bool isLeftClimb;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        _head = transform.GetChild(0);
    }

    void Update()
    {

        currspeed = speed * Input.GetAxis("Horizontal");
        vertspeed = speed * Input.GetAxis("Vertical");
        if (currspeed < 0)
        {
            _head.localScale = new Vector3(1, -1, 1);
        }
        else if (currspeed > 0)
        {
            _head.localScale = Vector3.one;

        }
        if (canClimb)
        {
            transform.position = new Vector3(transform.position.x + currspeed * Time.deltaTime, transform.position.y + vertspeed * Time.deltaTime, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + currspeed * Time.deltaTime, transform.position.y , transform.position.z);
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                Jump();
            }
            
        }
        CalculateGround();


    }
    public void Jump()
    {
        if (!GetComponent<Player>().isUpsideDown)
        {
            float force = 20;
            if (RB.velocity.y < 0)
                force -= RB.velocity.y;

            RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            canJump = false;
           

        }
        else
        {
            float force = -20;
            if (RB.velocity.y < 0)
                force -= RB.velocity.y;

            RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            canJump = false;
        }
    }
    public void CalculateGround()
    {
        float offset = 0;
        offset = yOffest + offsetTimer;
        if (currspeed == 0)
        {
            offset = yOffest + offsetTimer;
        }
        else
        {
            offset = yOffest;
        }

        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, Vector3.down, groundCheckDistance, groundLayer);
        RaycastHit2D hitright = Physics2D.Raycast(rayPoint.position, Vector3.right, groundCheckDistance, groundLayer);
        RaycastHit2D hitleft = Physics2D.Raycast(rayPoint.position, Vector3.left, groundCheckDistance, groundLayer);
        if (hit.collider != null || hitright.collider != null || hitleft.collider != null)
        {
            Vector3 point = Vector3.zero;
            for (int i = 0; i < legTargets.Length; i++)
            {
                point += legTargets[i].position; // gets all the legtargets positions
            }
            point.x = point.x / legTargets.Length;
            point.x += offset;
            point.y = point.y / legTargets.Length;
            point.y += offset;
            transform.position = new Vector3(point.x, point.y, transform.position.z);
           
        }
        


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (collision.CompareTag("Wall"))
            {
                canClimb= true;
                canJump = true;
                if (collision.transform.position.x > transform.position.x)
                {
                    isLeftClimb = true;
                    isRightClimb = false;
                }
                else
                {
                    isLeftClimb = false;
                    isRightClimb = true;
                }
                RB.gravityScale = 0;
            }
            else
            {
                canClimb = false;
                isLeftClimb = false;
                isRightClimb = false;
                canJump = true;
                if (!GetComponent<Player>().isUpsideDown)
                {
                    RB.gravityScale = 1;

                }
                else
                {
                    RB.gravityScale = -1;
                }
            }

           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (collision.CompareTag("Wall"))
            {
                canClimb = false;
                isLeftClimb = false;
                isRightClimb = false;
                canJump = true;
                if (!GetComponent<Player>().isUpsideDown)
                {
                    RB.gravityScale = 1;

                }
                else {

                    RB.gravityScale = -1;
                }
            }
           


        }
    }


}