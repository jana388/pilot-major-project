using UnityEngine;

public class PlayerInput : MonoTimeBehaviour
{
    public PlayerMovement movement;
    public PickUp pickup;
    public Puzzle puzzle;
    enum PlayState
    {
        Explore,
        Puzzle
    }
    PlayState current;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = PlayState.Explore;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Hello");
        }
        switch (current)
        {
            case PlayState.Explore:
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                movement.SetHorizontalMovement(horizontal);
                movement.SetVerticalMovement(vertical);
                //if (Input.GetKeyDown(KeyCode.Space))
                //{
                //    if (pickup.PickUpObject())
                //    {
                //        current = PlayState.Puzzle;
                //        puzzle.Begin();
                //    }
                //}
                break;
            case PlayState.Puzzle:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Hello");
                    if (puzzle.SolvePuzzle())
                    {
                        current = PlayState.Explore;
                        puzzle.End();
                    }
                }
                break;

        }
    }
}