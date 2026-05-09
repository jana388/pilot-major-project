using UnityEngine;

public class MorleyAppears : MonoBehaviour
{
    public Animator morleyAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            morleyAnimator.SetTrigger("Appears");
        }
    }

}