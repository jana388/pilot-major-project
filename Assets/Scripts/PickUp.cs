using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PickUp : MonoBehaviour
{

    bool canpickup; 
    GameObject ObjectIwantToPickUp; 
    bool hasItem;
    List<GameObject> items = new();
   
    void Start()
    {
        canpickup = false;    
        hasItem = false;
    }

    [SerializeReference] InputActionReference interactAction;


    private void OnEnable()
    {
        interactAction.action.Enable();
        //jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        interactAction.action.Disable();
        //jumpAction.action.Disable();
    }

    private void Update()
    {
        if (interactAction.action.WasCompletedThisFrame())
        {
            if (canpickup && ObjectIwantToPickUp != null)
            {
                items.Add(ObjectIwantToPickUp);
                ObjectIwantToPickUp.SetActive (false);
                ObjectIwantToPickUp = null;
            }
        }
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

    public bool HaveYouGotTheItem(string itemName)
    {

        foreach (GameObject item in items)
        {
            if (item.tag == itemName)
            {
                return true;
            }
        }
        return false;
    }

    public bool DeleteItem(string itemName)
    {

        foreach (GameObject item in items)
        {
            if (item.tag == itemName)
            {
                items.Remove(item);
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "object")
        {
            canpickup = true;  //set the pick up bool to true
            ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
        }
        if (other.gameObject.tag == "Key")
        {
            canpickup = true;
            ObjectIwantToPickUp = other.gameObject;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "object")
        {
            canpickup = false;  //set the pick up bool to true
            ObjectIwantToPickUp = null; //set the gameobject you collided with to one you can reference
        }
        if (other.gameObject.tag == "Key")
        {

            canpickup = false;  //set the pick up bool to true
            ObjectIwantToPickUp = null;

        }
    }
}

