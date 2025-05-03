using System;
using UnityEngine;
using UnityEngine.Video;

[Serializable]
public struct UIPage
{
    public string name;
    public GameObject[] objs;
}
public enum GAME_STATUS
{
    IDLE = 0,
    INSTRUCTION,
    GAMEPLAY,
    ENDSCREEN
}
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    public GAME_STATUS m_status;
    [Space]
    [SerializeField] VideoPlayer transitionVideo;
    [SerializeField] int transitionDelay = 300;
    [Space]
    [SerializeField] UIPage[] screens;
    public GAME_STATUS GetCurrentStatus => m_status;
    public Action<GAME_STATUS> OnGameStatusChange;
    public Action<int, int> OnChangeStarted;
    public Action<int, int> OnChangeEnded;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        //GameManager.Instance.OnGameOver += OnGameOver;
#if UNITY_EDITOR
        ChangeStatus(m_status);
#else
        ChangeStatus(GAME_STATUS.IDLE);
#endif
    }

    private void OnGameOver(bool obj)
    {
        ChangeStatus(GAME_STATUS.ENDSCREEN);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ChangeStatus(GAME_STATUS.IDLE);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ChangeStatus(GAME_STATUS.INSTRUCTION);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ChangeStatus(GAME_STATUS.GAMEPLAY);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            ChangeStatus(GAME_STATUS.ENDSCREEN);
        }
    }

    public void HideObjs()
    {
        foreach (var screen in screens)
        {
            foreach (var child in screen.objs)
            {
                child.SetActive(false);
            }
        }
    }

    public void ShowItems(int p_listIndex)
    {
        var objs = screens[p_listIndex].objs;

        foreach (var child in objs)
        {
            child.SetActive(true);
        }
    }

    public void ChangeStatus(GAME_STATUS p_status = GAME_STATUS.IDLE)
    {
        OnChangeStarted?.Invoke((int)m_status, (int)p_status);
        OnGameStatusChange?.Invoke(p_status);

        m_status = p_status;
        //OnChangeEnded?.Invoke((int)m_status);
        switch (p_status)
        {
            case GAME_STATUS.IDLE:
                break;
            case GAME_STATUS.INSTRUCTION:
                break;
            case GAME_STATUS.GAMEPLAY:
                break;
            case GAME_STATUS.ENDSCREEN:
                break;
            default:
                break;
        }
        //await Task.Delay(1000);
        HideObjs();//hides and shows
        ShowItems((int)p_status);
        //await Task.Delay(1000);
    }

    public void ChangeStatus(int p_statusIndex)
    {
        ChangeStatus((GAME_STATUS)p_statusIndex);
    }

    public void PlayTransitionVideo()
    {
        transitionVideo.gameObject.SetActive(true);
        transitionVideo.Play();
    }

}