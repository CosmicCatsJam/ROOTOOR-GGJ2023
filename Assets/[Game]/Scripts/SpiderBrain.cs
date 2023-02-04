using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float offsetTimer;
    bool decrease;
    [Range(0f, 0.5f)]
    public float breatheHeight;
    public float breatheSpeed;
    public LegMover[] legs;
    Rigidbody2D RB;
    bool canJump;


    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        _head = transform.GetChild(0);
    }

    void Update()
    {

        currspeed = speed * Input.GetAxis("Horizontal");
        if (currspeed < 0)
        {
            _head.localScale = new Vector3(1, -1, 1);
        }
        else if (currspeed > 0)
        {
            _head.localScale = Vector3.one;

        }
        transform.position = new Vector3(transform.position.x + currspeed * Time.deltaTime, transform.position.y, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }
        CalculateGround();
       
    }
    public void Jump()
    {
        float force = 5;
        if (RB.velocity.y < 0)
            force -= RB.velocity.y;

        RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        canJump = false;
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
        if (hit.collider != null)
        {
            Vector3 point = Vector3.zero;
            for (int i = 0; i < legTargets.Length; i++)
            {
                point += legTargets[i].position; // gets all the legtargets positions
            }
            point.y = point.y / legTargets.Length;
            point.y += offset;
            transform.position = new Vector3(transform.position.x, point.y, transform.position.z);
           
        }
        else
        {
            //canJump = false;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canJump = true;
        }
    }
    


}