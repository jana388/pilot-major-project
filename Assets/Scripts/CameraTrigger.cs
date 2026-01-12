using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;    
using UnityEngine.Rendering;

[RequireComponent(typeof(BoxCollider))]
[ExecuteAlways]

public class CameraTrigger : MonoBehaviour
{
   [SerializeField] public CinemachineCamera cam;
   [SerializeField] private Vector3 boxSize;

   private BoxCollider box;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
        ApplyBoxSize();
    }

    private void OnValidate()
    {
        if (box == null) box = GetComponent<BoxCollider>();
        ApplyBoxSize();
    }

    private void ApplyBoxSize()
    {
        if (box != null)
        {
            box.isTrigger = true;
            box.size = boxSize;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraSwitcher.SwitchCamera(cam);
        }
    } 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(box.center, box.size);
    }
}
