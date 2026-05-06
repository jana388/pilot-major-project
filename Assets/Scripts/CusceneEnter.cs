using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class CusceneEnter : MonoBehaviour
{
    public GameObject player;
    public GameObject cutsceneCam;
    public Transform cutscenePlayerPosition; 
    public AudioSource liftSound;       
    public float cutsceneDuration = 4f;
    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        cutsceneCam.SetActive(true);

        var input = player.GetComponent<PlayerInput>();
        if (input != null) input.enabled = false;

        player.transform.SetPositionAndRotation(
            cutscenePlayerPosition.position,
            cutscenePlayerPosition.rotation
        );

        StartCoroutine(CutsceneRoutine());

        if (liftSound != null)
            liftSound.Play();
        
    }

    private IEnumerator CutsceneRoutine()
    {
        yield return new WaitForSeconds(cutsceneDuration);

        SceneManager.LoadScene("GameCompleted");
    }
}

