using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutSceneEnder : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("GameCompleted");
    }
}

