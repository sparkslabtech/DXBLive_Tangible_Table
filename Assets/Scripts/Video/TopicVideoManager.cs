using DG.Tweening.Plugins.Core.PathCore;
using RenderHeads.Media.AVProVideo;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TopicVideoManager : MonoBehaviour
{
    private static TopicVideoManager instance;
    public static TopicVideoManager GetInstance => instance;

    [Header("Video Players")]
    [SerializeField] MediaPlayer m_screenSaverPlayer;
    [SerializeField] MediaPlayer m_videoPlayer;
    [Header("File Reader Param")]
    [SerializeField] string[] m_fileType = { ".mp4", ".webm" };
    [Header("FilePaths")]
    [SerializeField] string m_screenSaverPath = "Screen Saver.mp4";
    [SerializeField] string m_marker1FolderPath = "Marker1";
    [SerializeField] string m_marker2FolderPath = "Marker2";
    [SerializeField] string[] m_videoFilePathsMarker1;
    [SerializeField] string[] m_videoFilePathsMarker2;
    [Header("Values")]
    [SerializeField] string m_currentlyPlaying;
    [SerializeField] bool reverseVideos = true;
    int selectedMarker = -1;
    int selectedIndex = -1;
    public string ImageRoot = $"{Application.streamingAssetsPath}\\Videos Topics";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        ImageRoot = $"{Application.streamingAssetsPath}\\Videos Topics";
        m_videoFilePathsMarker1 = FindVideoFiles($"{ImageRoot}\\{m_marker1FolderPath}");
        m_videoFilePathsMarker2 = FindVideoFiles($"{ImageRoot}\\{m_marker2FolderPath}");
        m_videoPlayer.Events.AddListener(OnTopicVideoEvent);
        ResetSelection();
    }

    void ResetSelection()
    {
        selectedMarker = -1;
        selectedIndex = -1;
        m_currentlyPlaying = "";
    }

    private void OnDisable()
    {
        m_videoPlayer.CloseMedia();
    }

    private void OnTopicVideoEvent(MediaPlayer p_mediaPlayer, MediaPlayerEvent.EventType p_eventType, ErrorCode p_err)
    {
        switch (p_eventType)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                m_videoPlayer.gameObject.SetActive(true);
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                //m_videoPlayer.gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }
    bool selected = false;

    public void PlayTopicVideo(int p_markerIndex, int p_selectedIndex)
    {
        if (p_markerIndex == selectedMarker && selectedIndex == p_selectedIndex)
            return;

        var markerType = p_markerIndex == 0 ? m_videoFilePathsMarker1 : m_videoFilePathsMarker2;
        if (p_selectedIndex < 0 || p_selectedIndex > markerType.Length)
        {
            p_selectedIndex = 0;
        }
        var path = markerType[p_selectedIndex];

        if (path.Equals(m_currentlyPlaying))
            return;

        m_videoPlayer.CloseMedia();
        m_videoPlayer.gameObject.SetActive(true);
        m_videoPlayer.OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, path, true);

        m_currentlyPlaying = path;
        selectedMarker = p_markerIndex;
        selectedIndex = p_selectedIndex;
    }

    private void PlayVideo(int p_markerIndex, int p_selectedIndex, string p_path)
    {
        m_videoPlayer.gameObject.SetActive(true);
        m_videoPlayer.OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, p_path, true);
        selectedMarker = p_markerIndex;
        selectedIndex = p_selectedIndex;
    }

    public string[] FindVideoFiles(string p_folderPath)
    {
        var di = new DirectoryInfo(p_folderPath);
        var list = new List<string>();

        foreach (var fileType in m_fileType)
        {
            foreach (var file in di.GetFiles($"*{fileType}", SearchOption.AllDirectories))
            {
                list.Add(file.ToString());
            }
        }
        if (reverseVideos)
            list.Reverse();

        return list.ToArray();
    }

    public void SetSelected(bool p_selected)
    {
        selected = p_selected;
    }

    internal void ShowScreenSaver()
    {
        if (m_videoPlayer.gameObject.activeSelf)
        {
            m_screenSaverPlayer.OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, m_screenSaverPath);
            m_screenSaverPlayer.Play();
        }
        m_videoPlayer.gameObject.SetActive(false);
        ResetSelection();
    }
}
