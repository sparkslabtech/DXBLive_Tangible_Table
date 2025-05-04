using EyeFactiveMarkerManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSwitch : MonoBehaviour
{
    public MarkerManager markerManager;
    [Header("Looping Video")]
    [SerializeField] VideoPlayer CurrentVideoPlayer;
    [SerializeField] VideoPlayer LoopVideoPlayer;
    [SerializeField] RenderTexture LoopingTexture;
    [Space]

    //[Header("Looping Video Paths")]
    //[SerializeField] string[] LoopingVideoPaths;
    [Space]
    public string[] EngvideoPaths;
    public string[] ArbvideoPaths;
    [SerializeField] string CurrentPathPlaying;
    [SerializeField] int CurrentIndexPlaying;
    [Header("Fade Settings"), Space]
    public CanvasGroup videoCanvasGroup;
    public float fadeDuration = 1f;

    void Start()
    {
        LoopVideoPlayer.gameObject.SetActive(false);
        
        EngvideoPaths = LoopingVideoPaths.GetInstance().GetEngPaths();
        ArbvideoPaths = LoopingVideoPaths.GetInstance().GetArbPaths();
        markerManager.OnNoPucksDetected += OnAllPucksRemoved;
    }
    private void OnAllPucksRemoved()
    {
        Debug.Log("Puck Removed");
        LoopVideoPlayer.gameObject.SetActive(false);
        LoopVideoPlayer.Stop();
    }
    public void SwitchVideoOnEnd(int index, int language)
    {
        Debug.Log("Video Switched");
        switch(language) 
        {
            case 0:
                {
                    CurrentVideoPlayer.gameObject.SetActive(false);
                    LoopVideoPlayer.gameObject.SetActive(true);

                    CurrentIndexPlaying = index;
                    CurrentPathPlaying = EngvideoPaths[index];

                    //LoopVideoPlayer.Stop();
                    LoopVideoPlayer.url = CurrentPathPlaying;
                    LoopVideoPlayer.Play();
                }
                break;

            case 1:
                {
                    CurrentVideoPlayer.gameObject.SetActive(false);
                    LoopVideoPlayer.gameObject.SetActive(true);

                    CurrentIndexPlaying = index;
                    CurrentPathPlaying = ArbvideoPaths[index];

                    //LoopVideoPlayer.Stop();
                    LoopVideoPlayer.url = CurrentPathPlaying;
                    LoopVideoPlayer.Play();
                }
                break;

        }
    }
}
