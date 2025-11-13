using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;    
using UnityEngine.Rendering;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent (typeof(Rigidbody))]


public class CameraTrigger : MonoBehaviour
{
   [SerializeField] public CinemachineCamera cam;
   [SerializeField] private Vector3 boxSize;

    BoxCollider box;
    Rigidbody rb;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        box.isTrigger = true;
        box.size = boxSize;

        rb.isKinematic = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if (CameraSwitcher.ActiveCamera != cam) CameraSwitcher.SwitchCamera(cam);
            cam.Priority = 12;
        }
    }
}
