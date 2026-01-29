using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{

    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();

    public static event System.Action<CinemachineCamera> OnCameraSwitched;

    public static CinemachineCamera ActiveCamera = null;

    // This is stacking priority counter
    private static int nextPriority = 10;


    public static bool IsActiveCamera(CinemachineCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineCamera camera)
    {
        // Increase priority each time a camera is activated
        nextPriority++;

        camera.Priority = nextPriority;
        ActiveCamera = camera;

        // send the info about the camera switch to the player controller
        OnCameraSwitched?.Invoke(camera);
        // not zeroing the cameras out because we have the stacking method now (at least for now) 

    }

    public static void Register(CinemachineCamera camera)
    {
        if (!cameras.Contains(camera))
        {
            cameras.Add(camera);
            Debug.Log("Camera registered: " + camera);
        }

    }

    public static void Unregister(CinemachineCamera camera)
    {
        if (cameras.Contains(camera))
        {
            cameras.Remove(camera);
            Debug.Log("Camera unregistered: " + camera);
        }

    }

}

