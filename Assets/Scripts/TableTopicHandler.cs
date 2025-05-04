using DG.Tweening;
using EyeFactiveMarkerManager;
using UnityEngine;
using UnityEngine.Video;

public class TableTopicHandler : MonoBehaviour
{
    public MarkerManager markerManager;
    public RenderTexture rt;
    public VideoPlayer player;
    //[Space]
    //public VideoPlayer TableBG;
    [Space]
    [Header("Table UI")]
    public GameObject Logo;
    public GameObject MarkerPlacement;
    [Space]
    [Header("Fade Settings")]
    public CanvasGroup videoCanvasGroup;
    public float fadeDuration = 1f;
    [Space]
    public string[] EngvideoPaths;
    public string[] ArbvideoPaths;
    public string currentPathPlaying;
    public int currentIndexPlaying = 0;

    void Start()
    {
        currentIndexPlaying = -1;
        rt.Release();

        EngvideoPaths = TableTopicVideoPath.GetInstance().GetEngPaths();
        ArbvideoPaths = TableTopicVideoPath.GetInstance().GetArbPaths();
        //TableBG.gameObject.SetActive(true);

        markerManager.OnNoPucksDetected += OnAllPucksRemoved;
    }

    private void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    rt.Release();
        //    player.Stop();
        //    player.gameObject.SetActive(false);
        //    //TableBG.gameObject.SetActive(false);
        //    currentIndexPlaying = -1;
        //}
    }

    private void OnAllPucksRemoved()
    {
        rt.Release();
        Debug.Log("All Puck Removed Called");
        player.Stop();
        //player.gameObject.SetActive(false);
        //TableBG.gameObject.SetActive(true);
        Logo.SetActive(true);
        MarkerPlacement.SetActive(true);
        //throw new NotImplementedException();
        currentIndexPlaying = -1;


        //videoCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
        //{
        //    videoCanvasGroup.gameObject.SetActive(false);
        //});

        Debug.Log($"No  Puck Detected");
    }


    public void PlayTopic(int p_index)
    {
        if (currentIndexPlaying == p_index)
        {
            return;
        }
        if (EngvideoPaths.Length <= 0)
        {
            EngvideoPaths = TableTopicVideoPath.GetInstance().GetEngPaths();
            ArbvideoPaths = TableTopicVideoPath.GetInstance().GetArbPaths();
        }

        rt.Release();

        //TableBG.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        Logo.SetActive(false);
        MarkerPlacement.SetActive(false);

        Debug.Log($"Now Table Playing: {p_index}");
        currentIndexPlaying = p_index;
        currentPathPlaying = EngvideoPaths[p_index];

        player.Stop();
        player.url = currentPathPlaying;
        player.Play();


        videoCanvasGroup.gameObject.SetActive(true);
        videoCanvasGroup.alpha = 0f;
        videoCanvasGroup.DOFade(1f, fadeDuration);
    }

    internal void PlayTopic(int currentSelected, int p_language)
    {
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
                        Debug.LogError("Stopped Video Player");
                        return;
                    }
                    if (EngvideoPaths.Length <= 0)
                    {
                        EngvideoPaths = TableTopicVideoPath.GetInstance().GetEngPaths();
                    }

                    Debug.LogError("VideoPlayer Entered eng table");
                    rt.Release();

                    //TableBG.gameObject.SetActive(false);
                    player.gameObject.SetActive(true);
                    Logo.SetActive(false);
                    MarkerPlacement.SetActive(false);

                    //Debug.Log($"Now Table Playing: {currentSelected}");
                    currentIndexPlaying = currentSelected;
                    currentPathPlaying = EngvideoPaths[currentSelected];

                    player.Stop();
                    player.url = currentPathPlaying;
                    player.Play();


                    videoCanvasGroup.gameObject.SetActive(true);
                    videoCanvasGroup.alpha = 0f;
                    videoCanvasGroup.DOFade(1f, fadeDuration);
                }
                break;
            case 1://ARABIC
                if (currentPathPlaying.Contains("Arb"))
                {
                    Debug.LogWarning($"Already Playing Arabic");
                    Debug.LogError("Stopped Video Player");
                    return;
                }
                //if (currentIndexPlaying == currentSelected)
                //{
                //    return;
                //}
                if (EngvideoPaths.Length <= 0)
                {
                    EngvideoPaths = TableTopicVideoPath.GetInstance().GetEngPaths();
                }

                Debug.LogError("VideoPlayer Entered ar table");
                rt.Release();

                //TableBG.gameObject.SetActive(false);
                player.gameObject.SetActive(true);
                Logo.SetActive(false);
                MarkerPlacement.SetActive(false);

                //Debug.Log($"Now Table Playing: {currentSelected}");
                currentIndexPlaying = currentSelected;
                currentPathPlaying = ArbvideoPaths[currentSelected];

                player.Stop();
                player.url = currentPathPlaying;
                player.Play();


                videoCanvasGroup.gameObject.SetActive(true);
                videoCanvasGroup.alpha = 0f;
                videoCanvasGroup.DOFade(1f, fadeDuration);

                break;
            default:
                Debug.LogError($"No videos found!!");
                break;
        }
        Debug.Log($"Now Table Playing: topic{currentSelected} language: {p_language}");

    }
#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.TextArea(new Rect(500, 0, 100, 200), $"current path: {currentPathPlaying} topic : {currentIndexPlaying}");
    }
#endif
}
