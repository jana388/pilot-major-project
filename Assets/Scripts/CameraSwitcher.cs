using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using UnityEditor.PackageManager;

public class CameraSwitcher : MonoBehaviour
{

    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();

    public static CinemachineCamera ActiveCamera = null;

    public static bool IsActiveCamera(CinemachineCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineCamera camera)
    {
        camera.Priority = 0;
        ActiveCamera = camera;

        foreach (CinemachineCamera c in cameras)
        {
            if (c != camera && c.Priority != 0)
            {
                c.Priority = 0;
            }
        }
    }

    public static void Register(CinemachineCamera camera)
    {
        cameras.Add(camera);
        Debug.Log("Camera registered:" + camera);
    }

    public static void Unregister(CinemachineCamera camera)
    {
        cameras.Remove(camera);
        Debug.Log("Camera unregistered:" + camera);
    }

}

