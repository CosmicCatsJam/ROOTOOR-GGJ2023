using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform Leg_1_Effector;
    public Transform Leg_2_Effector;
    public Transform Leg_3_Effector;
    public Transform Leg_4_Effector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interacteables")
        {
            collision.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interacteable")
        {
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interacteables")
        {
            collision.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
