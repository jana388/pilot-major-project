using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3,10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool playerDetected;

    private InputAction interactAction;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
        // I want to Disable Puzzle & PlayerMovement so that the player can interact with spacebar withou doing anything in the game
    }

    private void Awake()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            playerDetected = true;
            //TriggerDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerDetected = false;
            //TriggerDialogue();
        }
    }

    private void Update()
    {
        if (playerDetected && interactAction.IsPressed())
            TriggerDialogue();
    }

}
