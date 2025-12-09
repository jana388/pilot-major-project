using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    public GameObject dialogueBox;

    private Queue<DialogueLine> lines = new();

    public bool isDialogueActive = false;

    public float typingSpeed = 0.2f;

    public Animator animator;

    private Coroutine dialogueRoutine;
    
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            dialogueBox.SetActive(false);
        }

    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        dialogueBox.SetActive(true);
        if (animator)
        {
            animator.Play("show");
        }
        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void CloseDialogue()
    {
        if (dialogueRoutine != null) StopCoroutine(dialogueRoutine);

        isDialogueActive = false;
        dialogueBox.SetActive(false);
        if (animator)
        {
            animator.Play("hide");
        }
    }

    public void DisplayNextDialogueLine()
    {
        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        dialogueRoutine = StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds (typingSpeed);
        }
    }

    void Update()
    {
        if (isDialogueActive && lines.Count == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CloseDialogue();
            }
        }
    }
}