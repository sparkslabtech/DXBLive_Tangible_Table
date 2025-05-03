using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    public string videoFileName = "Waves.mp4"; // Name of the video file in StreamingAssets
    public RenderTexture renderTexture;
    private VideoPlayer videoPlayer;

    private void OnDisable()
    {
        renderTexture.Release();
    }

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        renderTexture = videoPlayer.targetTexture;
        if (videoPlayer == null)
        {
            Debug.LogError("No VideoPlayer component found on this GameObject.");
            return;
        }

        LoadAndPlayVideo();
    }

    public void LoadAndPlayVideo()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, videoFileName);

        if (File.Exists(filePath))
        {
            videoPlayer.url = filePath;
            videoPlayer.Play();
            Debug.Log("Playing video: " + filePath);
        }
        else
        {
            Debug.LogError("Video file not found at: " + filePath);
        }
    }

    public void LoadAndPlayVideo(string p_videoFileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, p_videoFileName);

        if (File.Exists(filePath))
        {
            videoPlayer.url = filePath;
            renderTexture.Release();
            videoPlayer.Play();
            Debug.Log("Playing video: " + filePath);
        }
        else
        {
            Debug.LogError("Video file not found at: " + filePath);
        }
    }
}
