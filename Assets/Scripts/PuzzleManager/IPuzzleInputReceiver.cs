using UnityEngine;
 public interface IPuzzleInputReceiver
 {
  void OnRotateInput(float input);
  void OnMoveInput(float input);
  void OnSubmitInput();
  void OnCancelInput();

}


