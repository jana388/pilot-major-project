using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{

    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();

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

        // No need to zero out other cameras — stacking handles it

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

