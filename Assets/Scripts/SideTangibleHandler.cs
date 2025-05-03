using System;
using UnityEngine;

public class SideTangibleHandler : MonoBehaviour
{
    public enum Page
    {
        VIDEOPLAYER = 0,
        SLIDESHOW,
        STATS,
    }
    public enum Language
    {
        ENGLISH, ARABIC
    }

    public Language m_selectedLanguage;
    public TOPIC_SIDE m_topicSide;
    public Page m_status;

    public GameObject m_infographicParent;
    [SerializeField] UIPage[] screens;
    public Action<Page> OnGameStatusChange;

    public TempTitleVideos TitleVideos;
    public TempStatsImages tempStatsImages;
    public SlideshowHandler slideshowHandler;
    public TempSlideshowImages m_tempSlideshowImages;

    private void OnEnable()
    {
        ChangeStatus(Page.VIDEOPLAYER);
    }

    private void Start()
    {
        //gameObject.SetActive(false);
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
    public bool isShowing => gameObject.activeInHierarchy;


    public void ShowSlideShow()
    {
        ChangeStatus(Page.SLIDESHOW);
    }

    public void ShowStat()
    {
        ChangeStatus(Page.STATS);
    }

    public void ShowVideoPlayer()
    {
        ChangeStatus(Page.VIDEOPLAYER);
    }

    public void HideSlideShow()
    {
    }

    public void ChangeTheStatus(int p_index)
    {
        ChangeStatus((Page)p_index);
    }

    public void ChangeStatus(Page p_status = Page.VIDEOPLAYER)
    {
        //OnChangeStarted?.Invoke((int)m_status, (int)p_status);
        OnGameStatusChange?.Invoke(p_status);

        m_status = p_status;
        //OnChangeEnded?.Invoke((int)m_status);
        switch (p_status)
        {
            case Page.VIDEOPLAYER:
                break;
            case Page.SLIDESHOW:

                m_tempSlideshowImages.AssignSlideShowSprites(1);
                break;
            case Page.STATS:
                tempStatsImages.ShowStatRender(1);
                    break;
            default:
                break;
        }       //await Task.Delay(1000);
        HideObjs();//hides and shows
        ShowItems((int)p_status);
        //await Task.Delay(1000);
    }
    public void ShowItems(int p_listIndex)
    {
        var objs = screens[p_listIndex].objs;

        foreach (var child in objs)
        {
            child.SetActive(true);
        }
    }

}
