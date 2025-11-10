using UnityEngine;

public class PickUp : MonoBehaviour
{

    bool canpickup; 
    GameObject ObjectIwantToPickUp; 
    bool hasItem;
   
    void Start()
    {
        canpickup = false;    
        hasItem = false;
    }

    // Update is called once per frame
    public bool PickUpObject()
    {
        if (canpickup) 
        {
            ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
            ObjectIwantToPickUp.SetActive(false);
            canpickup = false;
            hasItem = true;
            ObjectIwantToPickUp = null;
            return true;
        }
        return false;
    }
    //private void OnCollisionEnter(Collision other) 
    //{
    //    if (other.gameObject.tag == "object") 
    //    {
    //        canpickup = true;  //set the pick up bool to true
    //        ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
    //    }
    //}
    //private void OnCollisionExit(Collision other)
    //{
    //    canpickup = false; 

    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "object")
        {
            canpickup = true;  //set the pick up bool to true
            ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "object")
        {
            canpickup = false;  //set the pick up bool to true
            ObjectIwantToPickUp = null; //set the gameobject you collided with to one you can reference
        }
    }
}

