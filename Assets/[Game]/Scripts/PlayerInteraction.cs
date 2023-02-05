using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LegMover LefttLeg_2;
    public LegMover RightLeg_3;
    Transform left_2_Target;
    Transform left_2_OriginalParent;
    Vector3 left_2_OriginalPos;
    Transform right_3_Target;
    Transform right_3_OriginalParent;
    Vector3 right_3_OriginalPos;

    Vector3 SavedRootPos;
    Vector3 SavedBodyPos;

    bool isInteracted;
    bool isInteracteable;
    Transform currentInteracteable;
    private void Start()
    {
        left_2_Target = LefttLeg_2.legTarget;
        left_2_OriginalParent = left_2_Target.parent;
        left_2_OriginalPos = left_2_Target.localPosition;
        right_3_Target = RightLeg_3.legTarget;
        right_3_OriginalParent= right_3_Target.parent;
        right_3_OriginalPos = right_3_Target.localPosition;

        SavedRootPos = transform.root.position;
        SavedBodyPos = transform.localPosition;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interacteables")
        {
            collision.transform.GetChild(0).gameObject.SetActive(true);
            currentInteracteable = collision.transform;
            isInteracteable =true;
        }
        if (collision.gameObject.layer ==LayerMask.NameToLayer("Gate"))
        {
            EventManager.OnLevelUp.Invoke();
            collision.isTrigger = false;
            transform.root.position += new Vector3(5f, 0, 0);
            collision.gameObject.layer = LayerMask.NameToLayer("Ground");
            SavedRootPos = transform.root.position;
            SavedBodyPos = transform.localPosition;

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            transform.root.position = SavedRootPos;
            transform.localPosition =  SavedBodyPos;

        }
    }

    private void Update()
    {
        if (isInteracteable) {


            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isInteracted)
                {
                    Debug.Log("Connected");
                    isInteracted = true;
                    currentInteracteable.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    //Box sagda
                    if (currentInteracteable.transform.position.x > transform.position.x)
                    {
                        right_3_OriginalPos = right_3_Target.localPosition;
                        currentInteracteable.transform.SetParent(transform, true);
                        right_3_Target.transform.position = currentInteracteable.position;
                        right_3_Target.SetParent(RightLeg_3.transform, true);
                        
                    }
                    //Box solda
                    else
                    {
                        left_2_OriginalPos = left_2_Target.localPosition;
                        currentInteracteable.transform.SetParent(transform, true);
                        left_2_Target.transform.position=currentInteracteable.position;
                        left_2_Target.SetParent(LefttLeg_2.transform, true);
                    }


                }
                else
                {
                    currentInteracteable.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                    isInteracted = false;
                    //Box sagda
                    if (currentInteracteable.transform.position.x > transform.position.x)
                    {
                        right_3_Target.SetParent(right_3_OriginalParent, false);
                        right_3_Target.localPosition = right_3_OriginalPos;
                        currentInteracteable.transform.SetParent(null, true);
                    }
                    //Box solda
                    else
                    {
                        left_2_Target.SetParent(left_2_OriginalParent, false);
                        left_2_Target.localPosition = left_2_OriginalPos;
                        currentInteracteable.transform.SetParent(null, true);
                    }
                }

            }

        }
        else
        {
            if (isInteracted)
            {
                currentInteracteable.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                isInteracted = false;
                //Box sagda
                if (currentInteracteable.transform.position.x > transform.position.x)
                {
                    right_3_Target.SetParent(right_3_OriginalParent, false);
                    right_3_Target.localPosition = right_3_OriginalPos;
                    currentInteracteable.transform.SetParent(null, true);
                }
                //Box solda
                else
                {
                    left_2_Target.SetParent(left_2_OriginalParent, false);
                    left_2_Target.localPosition = left_2_OriginalPos;
                    currentInteracteable.transform.SetParent(null, true);
                }

            }
        }
    }
  
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interacteables")
        {
            collision.transform.GetChild(0).gameObject.SetActive(false);
            isInteracteable= false;
        }
    }
}
