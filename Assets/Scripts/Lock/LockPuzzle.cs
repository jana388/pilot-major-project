using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LockPuzzle : MonoBehaviour
{
    [Header("Puzzle Info")]
    [SerializeField] private string _puzzleCode;
    [SerializeField] private UnityEvent _unityEvent;

    [Header("Objects In Puzzle")]
    [SerializeField] private GameObject _lockInteractive;
    [SerializeField] private GameObject _lockPuzzle;
    [SerializeField] private GameObject _lockCam;


    //private InputActionMap puzzle;
    //private InputActionMap player;
    //private bool playerDetected;
    private bool puzzleStarts;
    private InputAction interactAction;

    private bool _puzzleStarts;
    private float _rotationStep = 36f;
    private int _currentCylinder = 0;

    private int _cylinder01Step = 0;
    [SerializeField] private GameObject _cylinder01;
    private int _cylinder02Step = 0;
    [SerializeField] private GameObject _cylinder02;
    private int _cylinder03Step = 0;
    [SerializeField] private GameObject _cylinder03;
    private string _cylinder01Letter = "";
    private string _cylinder02Letter = "";
    private string _cylinder03Letter = "";

    private void Start()
    {
        StartCoroutine(PuzzleStarts());
    }

    void Update()
    {
        if (_puzzleStarts == true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                _currentCylinder = Mathf.Min(_currentCylinder + 1, 2);
                //AudioManager.Instance.PlaySFXClip(0);  -- for adding sounds later 
            }

            if (Input.GetKey(KeyCode.A))
            {
                _currentCylinder = Mathf.Max(_currentCylinder - 1, 0);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                if (_currentCylinder == 0)
                {
                    _cylinder01Step = (_cylinder01Step + 1) % 10;
                    _cylinder01.transform.localEulerAngles = new Vector3(_cylinder01Step * 36f, 0f, 0f);
                    Cylinder01Values();
                }

                if (_currentCylinder == 1)
                {
                    _cylinder02Step = (_cylinder02Step + 1) % 10;
                    _cylinder02.transform.localEulerAngles = new Vector3(_cylinder02Step * 36f, 0f, 0f);
                    Cylinder02Values();
                }

                if (_currentCylinder == 2)
                {
                    _cylinder03Step = (_cylinder03Step + 1) % 10;
                    _cylinder03.transform.localEulerAngles = new Vector3(_cylinder03Step * 36f, 0f, 0f);
                    Cylinder03Values();
                }
                
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                if (_currentCylinder == 0)
                {
                    _cylinder01Step = (_cylinder01Step - 1 + 8) % 10;
                    _cylinder01.transform.localEulerAngles = new Vector3(_cylinder01Step * 36f, 0f, 0f);
                    Cylinder01Values();
                }

                if (_currentCylinder == 1)
                {
                    _cylinder02Step = (_cylinder02Step - 1 + 8) % 10;
                    _cylinder02.transform.localEulerAngles = new Vector3(_cylinder02Step * 36f, 0f, 0f);
                    Cylinder02Values();
                }

                if (_currentCylinder == 2)
                {
                    _cylinder03Step = (_cylinder03Step - 1 + 8) % 10;
                    _cylinder03.transform.localEulerAngles = new Vector3(_cylinder03Step * 36f, 0f, 0f);
                    Cylinder03Values();
                }
            }
        }

    }

    public void CheckCode()
    {
        string enteredCode = _cylinder01Letter + _cylinder02Letter + _cylinder03Letter;

        if (enteredCode == _puzzleCode)
        {
            StartCoroutine("Completed Puzzle");
            Debug.Log("Puzzle is complete Code: " + enteredCode);
        }

        else
        {
            Debug.Log("Code incorrect. Entered:" + enteredCode + "Expected:" + _puzzleCode);
        }
    }

    public void Cylinder01Values()
    {
        switch (_cylinder01Step)
        {
            case 0:
                _cylinder01Letter = "0";
                break;
            case 1:
                _cylinder01Letter = "1";
                break;
            case 2:
                _cylinder01Letter = "2";
                break;
            case 3:
                _cylinder01Letter = "3";
                break;
            case 4:
                _cylinder01Letter = "4";
                break;
            case 5:
                _cylinder01Letter = "5";
                break;
            case 6:
                _cylinder01Letter = "6";
                break;
            case 7:
                _cylinder01Letter = "7";
                break;
            case 8:
                _cylinder01Letter = "8";
                break;
            case 9: 
                _cylinder01Letter = "9";
                break;
            default:
                _cylinder01Letter = "0";
                break;

        }

        Debug.Log(" Cylinder is on  " + _cylinder01Step * 36 + "and the number is" + _cylinder01Letter);
        CheckCode();
    }

    public void Cylinder02Values()
    {
        switch (_cylinder02Step)
        {
            case 0:
                _cylinder02Letter = "0";
                break;
            case 1:
                _cylinder02Letter = "1";
                break;
            case 2:
                _cylinder02Letter = "2";
                break;
            case 3:
                _cylinder02Letter = "3";
                break;
            case 4:
                _cylinder02Letter = "4";
                break;
            case 5:
                _cylinder02Letter = "5";
                break;
            case 6:
                _cylinder02Letter = "6";
                break;
            case 7:
                _cylinder02Letter = "7";
                break;
            case 8:
                _cylinder02Letter = "8";
                break;
            case 9:
                _cylinder02Letter = "9";
                break;
            default:
                _cylinder02Letter = "0";
                break;

        }

        Debug.Log(" Cylinder is in " + _cylinder02Step+ "and the number is" + _cylinder02Letter);
        CheckCode();
    }

    public void Cylinder03Values()
    {
        switch (_cylinder03Step)
        {
            case 0:
                _cylinder03Letter = "0";
                break;
            case 1:
                _cylinder03Letter = "1";
                break;
            case 2:
                _cylinder03Letter = "2";
                break;
            case 3:
                _cylinder03Letter = "3";
                break;
            case 4:
                _cylinder03Letter = "4";
                break;
            case 5:
                _cylinder03Letter = "5";
                break;
            case 6:
                _cylinder03Letter = "6";
                break;
            case 7:
                _cylinder03Letter = "7";
                break;
            case 8:
                _cylinder03Letter = "8";
                break;
            case 9:
                _cylinder03Letter = "9";
                break;
            default:
                _cylinder03Letter = "0";
                break;

        }

        Debug.Log(" Cylinder is in " + _cylinder03Step + "and the number is" + _cylinder03Letter);
        CheckCode();
    }



    //private void OnTriggerEnter(Collider collider)
    //{
       // if (collider.tag == "Player")
       // {
          //  playerDetected = true;
          //  interactAction = InputSystem.actions.FindAction("Interact");

       // }
   // }

   // private void OnTriggerExit(Collider other)
   // {
       // if (other.tag == "Player")
      //  {
        //    playerDetected = false;
            
      //  }
   // }

    public void EndPuzzle()
    {
        _lockInteractive.SetActive(true);  
        _lockPuzzle.SetActive(false);
        _lockCam.SetActive(false);
        Debug.Log("Puzzle ended");
        _puzzleStarts = false;
        PlayerController.ActivateInputState(PlayerController.InputState.Player);

    }
    IEnumerator CompletedPuzzle()
    {
        //open lock animation
        yield return new WaitForSeconds(1.0f);
        _lockInteractive.SetActive(true);
        _lockPuzzle.SetActive(false);
        _lockCam.SetActive(false);
        _puzzleStarts = false;
    }

    IEnumerator PuzzleStarts()
    {
        Debug.Log("Puzzle starts");
        PlayerController.ActivateInputState(PlayerController.InputState.Puzzle);
        
        _lockCam.SetActive(true);
        _puzzleStarts = true;
        //_lockInteractive.SetActive(false);
        //_lockPuzzle.SetActive(true);
        //playerInput.SwitchCurrentActionMap("Puzzle"); 
        yield return new WaitForSeconds(0.2f); // small delay feels smoother
        //puzzleUI.Show();
        Debug.Log("Puzzle started");

        //yield return new WaitUntil(() => puzzle.IsSolved);

        //ExitPuzzle();
    }


    
}
