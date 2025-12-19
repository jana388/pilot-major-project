using UnityEngine;
using UnityEngine.Events;

public class Lock : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Dial[] dials;

    [Header("Settings")]
    [SerializeField] private string combination;

    [Header("Events")]
    [SerializeField] private UnityEvent onCorrectCombinationFound;

    [SerializeField] private float rotateAngle;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform parent;
    private float angle = 0;

    public void CheckCombination(Dial dial)
    {
        for (int i = 0; i < dials.Length; i++)
        {
            int combinationNumber = int.Parse(combination[i].ToString());

            if (combinationNumber != dials[i].GetNumber())
            {
                //dial.Unlock();
                return;
            }
        }
        CorrectCombination();
    }

    private void CorrectCombination()
    {
        for (int i = 0; i < dials.Length; i++)
        dials[i].Lock();

        onCorrectCombinationFound?.Invoke();
    }

    private void Update()
    {
        //angle = Time.deltaTime * rotationSpeed;
        //parent.Rotate(angle, 0, 0);
        if(
    }
}   
