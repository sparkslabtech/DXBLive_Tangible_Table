using DG.Tweening;
using DG.Tweening.Plugins;
using EyeFactiveMarkerManager;
using JetBrains.Annotations;
using System;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DXBLiveTopicHandler : MonoBehaviour
{
    public MarkerManager markerManager;
    public RenderTexture rt;
    public VideoPlayer topicPlayer;
    //[Space]
    //public VideoPlayer BGPlayer;
    [Space]
    [Header("Fade Settings")]
    public CanvasGroup videoCanvasGroup;
    public float fadeDuration = 1f;
    [Space]
    public string[] EngvideoPaths;
    public string[] ArbvideoPaths;
    public string currentPathPlaying;
    public int currentIndexPlaying = 0;
    public bool introPlayed = false;
    int language = 0;
    [Header("Switch Video To Loop")]
    public VideoSwitch VideoSwitch;

    void Start()
    {
        //topicPlayer.isLooping = false;
        currentIndexPlaying = -1;
        rt.Release();
        introPlayed = false;
        EngvideoPaths = VideoPathLoader.GetInstance().GetEngPaths();
        ArbvideoPaths = VideoPathLoader.GetInstance().GetArbPaths();

        //BGPlayer.gameObject.SetActive(true);

        topicPlayer.loopPointReached += TopicPlayer_loopPointReached;
        markerManager.OnNoPucksDetected += OnAllPucksRemoved;
    }

    private void TopicPlayer_loopPointReached(VideoPlayer source)
    {
        Debug.Log($"Source: {source}");
        if (introPlayed)
        {
            Debug.Log($"Switching the loop video");
            VideoSwitch.SwitchVideoOnEnd(currentIndexPlaying, language);
        }
        else
        {
            //continue playing loop
            Debug.Log($"continuing the loop video");

        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rt.Release();
            topicPlayer.Stop();
            topicPlayer.gameObject.SetActive(false);
            //BGPlayer.gameObject.SetActive(false);
            currentIndexPlaying = -1;
        }
    }

    private void OnAllPucksRemoved()
    {
        rt.Release();
        topicPlayer.Stop();
        //player.gameObject.SetActive(false);
        //BGPlayer.gameObject.SetActive(true);
        //Logo.SetActive(true);
        //MarkerPlacement.SetActive(true);
        //throw new NotImplementedException();
        currentIndexPlaying = -1;


        videoCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            videoCanvasGroup.gameObject.SetActive(false);
        });

        Debug.Log($"No  Puck Detected");
    }

    void Update()
    {

    }

    public void PlayTopic(int p_index)
    {
        if (currentIndexPlaying == p_index)
        {
            return;
        }
        if (EngvideoPaths.Length <= 0)
        {
            EngvideoPaths = VideoPathLoader.GetInstance().GetEngPaths();
            ArbvideoPaths = VideoPathLoader.GetInstance().GetArbPaths();
        }

        rt.Release();

        //BGPlayer.gameObject.SetActive(false);
        topicPlayer.gameObject.SetActive(true);
        //Logo.SetActive(false);
        //MarkerPlacement.SetActive(false);

        Debug.Log($"Now Table Playing: {p_index}");
        currentIndexPlaying = p_index;
        currentPathPlaying = EngvideoPaths[p_index];

        topicPlayer.Stop();
        topicPlayer.url = currentPathPlaying;
        topicPlayer.Play();


        videoCanvasGroup.gameObject.SetActive(true);
        videoCanvasGroup.alpha = 0f;
        videoCanvasGroup.DOFade(1f, fadeDuration);
    }

    internal void PlayTopic(int currentSelected, int p_language)
    {
        language = p_language;
        Debug.Log($"Language: {p_language}");
        switch (p_language)
        {
            case 0://ENGLISh
                {
                    //if (currentIndexPlaying == currentSelected)
                    //{
                    //    return;
                    //}
                    if (currentPathPlaying.Contains("Eng"))
                    {
                        Debug.LogWarning($"Already Playing English");
                        return;
                    }
                    if (EngvideoPaths.Length <= 0)
                    {
                        EngvideoPaths = VideoPathLoader.GetInstance().GetEngPaths();
                    }

                    rt.Release();

                    //BGPlayer.gameObject.SetActive(false);
                    topicPlayer.gameObject.SetActive(true);
                    //Logo.SetActive(false);
                    //MarkerPlacement.SetActive(false);

                    //Debug.Log($"Now Table Playing: {currentSelected}");
                    currentIndexPlaying = currentSelected;
                    currentPathPlaying = EngvideoPaths[currentSelected];

                    topicPlayer.Stop();
                    topicPlayer.url = currentPathPlaying;
                    topicPlayer.Play();

                    introPlayed = true;
                    
                    videoCanvasGroup.gameObject.SetActive(true);
                    videoCanvasGroup.alpha = 0f;
                    videoCanvasGroup.DOFade(1f, fadeDuration);
                }
                break;
            case 1://ARABIC
                if (currentPathPlaying.Contains("Arb"))
                {
                    Debug.LogWarning($"Already Playing Arabic");
                    return;
                }
                //if (currentIndexPlaying == currentSelected)
                //{
                //    return;
                //}
                if (EngvideoPaths.Length <= 0)
                {
                    ArbvideoPaths = TableTopicVideoPath.GetInstance().GetArbPaths();
                }

                rt.Release();

                //BGPlayer.gameObject.SetActive(false);
                topicPlayer.gameObject.SetActive(true);
                //Logo.SetActive(false);
                //MarkerPlacement.SetActive(false);

                //Debug.Log($"Now Screen Playing: {currentSelected}");
                currentIndexPlaying = currentSelected;
                currentPathPlaying = ArbvideoPaths[currentSelected];

                topicPlayer.Stop();
                topicPlayer.url = currentPathPlaying;
                topicPlayer.Play();

                //introPlayed = false;
                introPlayed = true;
                //if (!player.isPlaying)
                //{
                //    VideoSwitch.SwitchVideoOnEnd(currentSelected, p_language);
                //}


                videoCanvasGroup.gameObject.SetActive(true);
                videoCanvasGroup.alpha = 0f;
                videoCanvasGroup.DOFade(1f, fadeDuration);

                break;
            default:
                Debug.LogError($"No videos found!!");
                break;
        }
        Debug.Log($"Now Screen Playing: topic{currentSelected} language: {p_language}");

    }

    //private void OnGUI()
    //{
    //    GUI.TextArea(new Rect(0, 0, 100, 200), $"current path: {currentPathPlaying} topic : {currentIndexPlaying}");
    //}
}



/*
{
    public MarkerManager markerManager;
    public RenderTexture rt;
    public VideoPlayer player;
    [Space]
    //public VideoPlayer TableBG;
    [Space]
    [Header("Fade Settings")]
    public CanvasGroup videoCanvasGroup;
    public float fadeDuration = 1f;
    [Space]
    public string[] videoPaths;
    public string currentPathPlaying;
    public int currentIndexPlaying = 0;

    void Start()
    {
        currentIndexPlaying = -1;
        rt.Release();

        videoPaths = VideoPathLoader.GetInstance().GetEngPaths();
        //TableBG.gameObject.SetActive(false);

        markerManager.OnNoPucksDetected += OnAllPucksRemoved;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rt.Release();
            player.Stop();
            player.gameObject.SetActive(false);
            //TableBG.gameObject.SetActive(false);
            currentIndexPlaying = -1;
        }
    }

    private void OnAllPucksRemoved()
    {
        if (player.isPlaying && player.gameObject.activeInHierarchy)
        {
            rt.Release();
            player.Stop();
            //player.gameObject.SetActive(false);
            //TableBG.gameObject.SetActive(false);
            //throw new NotImplementedException();
            videoCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                videoCanvasGroup.gameObject.SetActive(false);
            });
            currentIndexPlaying = -1;
        }
        Debug.Log($"No  Puck Detected");
    }

    void Update()
    {

    }

    public void PlayTopic(int p_index)
    {
        if (currentIndexPlaying == p_index)
        {
            return;
        }
        if (videoPaths.Length <= 0)
        {
            videoPaths = VideoPathLoader.GetInstance().GetEngPaths();
        }

        rt.Release();
        //TableBG.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        Debug.Log($"Now Playing: {p_index}");

        currentIndexPlaying = p_index;
        currentPathPlaying = videoPaths[p_index];              
        //Debug.Log($"Now Playing: {currentIndexPlaying}");
        Debug.Log($"Now Playing: {currentPathPlaying}");
        player.Stop();
        player.url = currentPathPlaying;
        player.Play();

        //videoCanvasGroup.gameObject.SetActive(true);
        videoCanvasGroup.alpha = 0f;
        videoCanvasGroup.DOFade(1f, fadeDuration);
    }

    //public void SwitchTopicAfterIntro()
    //{
    //    currentPathPlaying = videoPaths[currentIndexPlaying + 1];
    //    player.Stop();
    //    player.url = currentPathPlaying;
    //    player.Play();
    //}

    
}
*/