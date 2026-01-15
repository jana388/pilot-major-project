using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    public GameObject dialogueBox;
    [SerializeField] private GameContext context;

    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private Queue<DialogueLine> lines = new();

    public bool isDialogueActive = false;

    public float typingSpeed = 0.02f; // you can tweak this

    public Animator animator;

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

        context.playerController.ActivateInputState(PlayerController.InputState.Dialogue);

        if (animator)
            animator.Play("show");

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
            lines.Enqueue(dialogueLine);

        DisplayNextDialogueLine();
    }

    public void CloseDialogue()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        context.playerController.ActivateInputState(PlayerController.InputState.Player);

        isDialogueActive = false;
        dialogueBox.SetActive(false);

        if (animator)
            animator.Play("hide");
    }

    public void DisplayNextDialogueLine()
    {
        // If the line is still typing, finish it instead of advancing
        if (isTyping)
        {
            FinishTypingCurrentLine();
            return;
        }

        // If no more lines, close dialogue
        if (lines.Count == 0)
        {
            CloseDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence(currentLine.line));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueArea.text = "";

        foreach (char letter in sentence)
        {
            // If player requested skip, break early
            if (!isTyping)
                break;

            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Ensure full line is shown
        dialogueArea.text = sentence;
        isTyping = false;
    }

    private void FinishTypingCurrentLine()
    {
        isTyping = false;
    }
}



