using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LiftPuzzleManager : MonoBehaviour, IPuzzleInputReceiver
    {
        [SerializeField] private List<int> correctSequence;
        private List<int> playerSequence = new List<int>();

        [SerializeField] private LiftController liftController;

  
    [SerializeField] private GameContext context;



    public void AddInput(int number)
        {
            playerSequence.Add(number);

            if (playerSequence.Count == correctSequence.Count)
            {
                if (IsCorrect())
                    StartCoroutine(Success());
                else
                    StartCoroutine(Failure());
            }
        }

        private bool IsCorrect()
        {
            for (int i = 0; i < correctSequence.Count; i++)
            {
                if (playerSequence[i] != correctSequence[i])
                    return false;
            }
            return true;
        }

        private IEnumerator Success()
        {
            // flash green
            yield return new WaitForSeconds(0.5f);

            // Tell global puzzle manager to end puzzle
            context.puzzleManager.ShowPuzzleSolved();

            liftController.MoveLift();
        }

        private IEnumerator Failure()
        {
            // flash red
            playerSequence.Clear();
            yield return new WaitForSeconds(0.5f);
        }

        // Required by IPuzzleInputReceiver
        public void OnRotateInput(float value) { }
        public void OnMoveInput(float value) { }
        public void OnSubmitInput() { }
        public void OnCancelInput()
        {
            // Optional: allow exiting the lift puzzle
            context.puzzleManager.EndPuzzle();
        }
    }