using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSwitch : MonoBehaviour
{
    [Header("Looping Video")]
    [SerializeField] VideoPlayer CurrentVideoPlayer;
    [SerializeField] VideoPlayer LoopVideoPlayer;
    [SerializeField] RenderTexture LoopingTexture;
    [Space]

    [Header("Looping Video Paths")]
    [SerializeField] string[] LoopingVideoPaths;
    [Space]

    [SerializeField] string CurrentPathPlaying;
    [SerializeField] int CurrentIndexPlaying;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
