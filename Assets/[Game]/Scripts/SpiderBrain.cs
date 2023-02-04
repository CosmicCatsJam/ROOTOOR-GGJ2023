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


    // Start is called before the first frame update
    void Start()
    {
        // PUT THIS ON THE BODY BONE
        RB = GetComponent<Rigidbody2D>();
        _head = transform.GetChild(0);
    }

    // Update is called once per frame
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        CalculateGround();
        Idle();
    }
    public void Jump()
    {
        float force = 5;
        if (RB.velocity.y < 0)
            force -= RB.velocity.y;

        RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
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
            //  offset = yOffest;
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


    }

    public void Idle()
    {
        if (offsetTimer < breatheHeight && decrease == false)
        {
            offsetTimer += Time.deltaTime * (breatheSpeed * 0.1f);
        }


        else if (offsetTimer > breatheHeight)
        {
            decrease = true;
        }

        if (offsetTimer > -breatheHeight && decrease == true)
        {
            offsetTimer -= Time.deltaTime * (breatheSpeed * 0.1f);
        }
        else if (offsetTimer < -breatheHeight && decrease == true)
        {

            decrease = false;
        }
    }
}