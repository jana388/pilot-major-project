using UnityEngine;

public class LiftFloor : MonoBehaviour
{

    [SerializeField] string _DisplayName;
    Animator LinkedAnimator;

    public string DisplayName => _DisplayName; 

    private void Awake()
    {
        LinkedAnimator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
