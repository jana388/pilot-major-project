using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    [SerializeField] private GameContext context;
    //[SerializeField] private Material outlineMaterial;
    private Outline outline;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
        // I want to Disable Puzzle & PlayerMovement so that the player can interact with spacebar without doing anything in the game
    }

    private void Awake()
    {
        interactAction = InputSystem.actions.FindAction("Interact");

        // Setup separate outline material
        //outlineMaterial = new Material(outlineMaterial);
        outline = gameObject.GetOrAddComponent<Outline>();
        outline.OutlineWidth = Settings.instance.outlineWidth;
        outline.enabled = false;

        // Assign the new outline material to this mesh renderer
        //if (!TryGetComponent<MeshRenderer>(out var meshRenderer)) meshRenderer = GetComponentInChildren<MeshRenderer>();
        //var mats = meshRenderer.materials.ToList();
        //mats.Add(outlineMaterial);
        //meshRenderer.SetMaterials(mats);
    }

    private const string outlineStringID = "_EnableOutline";
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            playerDetected = true;
            //outlineMaterial.SetInt(outlineStringID, 1); // Enable outline shader
            outline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerDetected = false;
            //outlineMaterial.SetInt(outlineStringID, 0); // Disable outline shader
            outline.enabled = false;
        }
    }

    private void Update()
    {
        if (playerDetected && context.playerController.inputState == PlayerController.InputState.Player && interactAction.WasPressedThisFrame())
        {
            TriggerDialogue();
        }

    }

}
