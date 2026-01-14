using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LetterChecker : MonoBehaviour
{
    public LetterChecker next;

    void Start()
    {
        transform.Rotate(0, 0, 90 * Random.Range(1, 3));
    }

    
    public LetterChecker SolvePuzzle()
    {
        transform.Rotate(new Vector3(0, 0, -90));
        float z = Mathf.RoundToInt(transform.eulerAngles.z % 360);
        return (z == 0) ? next : this;
    }
}
