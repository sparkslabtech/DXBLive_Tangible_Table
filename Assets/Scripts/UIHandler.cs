using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using RenderHeads;
using RenderHeads.Media.AVProVideo;

[Serializable]
public class DataPaths
{
    public string VideoPath;
    public string slideshowMainPath;
    public string imgPath;
}

public class UIHandler : MonoBehaviour, ITangibleTableContentManager
{
    private static UIHandler _instance;
    public static UIHandler Instance => _instance;

    [SerializeField] MediaPlayer displayTwoVideoPlayer;
    [SerializeField] SideTangibleHandler leftSideTopicHandler;
    [SerializeField] SideTangibleHandler rightSideTopicHandler;
    [Space]
    public Button englishL, englishR;
    public Button arabicL, arabicR;
    [Space]
    public GameObject languageButtonsR, languageButtonsL;

    public int selectedTopic = 0;
    public DataPaths[] m_dataPaths;


    public void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedTopic = 0;
        englishL.onClick.AddListener(() => { ShowInfographics(TOPIC_SIDE.LEFT, true); leftSideTopicHandler.m_selectedLanguage = SideTangibleHandler.Language.ENGLISH; });
        englishR.onClick.AddListener(() => { ShowInfographics(TOPIC_SIDE.RIGHT, true); leftSideTopicHandler.m_selectedLanguage = SideTangibleHandler.Language.ENGLISH; });
        arabicL.onClick.AddListener(() => { ShowInfographics(TOPIC_SIDE.LEFT, true); leftSideTopicHandler.m_selectedLanguage = SideTangibleHandler.Language.ARABIC; });
        arabicR.onClick.AddListener(() => { ShowInfographics(TOPIC_SIDE.RIGHT, true); leftSideTopicHandler.m_selectedLanguage = SideTangibleHandler.Language.ARABIC; });
    }

    public void AssignButtonBehavior()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }

    public void ShowLanguageSelect(int p_index, TOPIC_SIDE p_topicSide)
    {
        if (selectedTopic == p_index)
        {
            return;
        }
        switch (p_topicSide)
        {
            case TOPIC_SIDE.LEFT:
                languageButtonsL.SetActive(true);
                break;
            case TOPIC_SIDE.RIGHT:
                languageButtonsR.SetActive(true);
                break;
            default:
                break;
        }

    }

    public void ShowInfographics(TOPIC_SIDE p_side, bool p_show = true)
    {
        if (p_side.Equals(TOPIC_SIDE.LEFT))
        {
            leftSideTopicHandler.gameObject.SetActive(true);
        }
        else
        {
            rightSideTopicHandler.gameObject.SetActive(true);
        }
    }

    public void PlayVideo()
    {

    }

    public void PlayVideo(int p_index, TOPIC_SIDE p_side)
    {
        throw new System.NotImplementedException();
    }

    public void ShowUI(int p_index, TOPIC_SIDE p_side)
    {
        throw new System.NotImplementedException();
    }

    public void HideTopic(TOPIC_SIDE p_side)
    {
        throw new System.NotImplementedException();
    }

    public void HideTopics()
    {
        throw new System.NotImplementedException();
    }
}
