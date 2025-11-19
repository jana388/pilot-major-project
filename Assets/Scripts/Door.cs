using UnityEngine;

public class Door : MonoBehaviour
{
    bool touching = false;
    PickUp player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && touching)
        {
            if (player.HaveYouGotTheItem("Key"))
            {
                player.DeleteItem("Key");
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            touching = true;
            player = other.GetComponent<PickUp>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            touching = false;
        }
    }
}
