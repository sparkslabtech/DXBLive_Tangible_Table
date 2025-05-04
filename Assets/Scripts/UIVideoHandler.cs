using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVideoHandler : MonoBehaviour
{
    private static UIVideoHandler _instance;
    public static UIVideoHandler Instance => _instance;

    [SerializeField] DXBLiveTopicHandler topicHandler;
    [SerializeField] TableTopicHandler TableTopicHandler;
    public int currentSelected = 0;
    public void Awake()
    {

        if (_instance == null)
        {
            _instance = this;

        }

    }

    public void PlayTopic(int p_index)
    {
        //topicHandler.PlayTopic(p_index, 0); //:LED
        //TableTopicHandler.PlayTopic(p_index, 0); //Table
        //
        topicHandler.PlayTopic(p_index); //:LED
        TableTopicHandler.PlayTopic(p_index); //Table
        currentSelected = p_index;
    }

    public void PlayArabicTopic()
    {
        //do me
    }

    public void ChangeTopicLanguage(int p_language = 0)
    {
        //topicHandler.PlayTopic(currentSelected); //:LED
        //TableTopicHandler.PlayTopic(currentSelected); //Table
        topicHandler.PlayTopic(currentSelected, p_language); //Table
        TableTopicHandler.PlayTopic(currentSelected, p_language); //Table


    }

}
