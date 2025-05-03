using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    private static ContentManager _instance;
    public static ContentManager Instance => _instance;
    [SerializeField] GameObject[] m_contentList;

    private void Awake()
    {
        _instance = this;
    }

    public void ShowTopic(int p_index)
    {
        m_contentList[p_index].SetActive(true);
    }

    public void ShowTopic(int p_index, bool p_show)
    {
        var contentObj = m_contentList[p_index];
        if (contentObj.activeSelf == p_show)
            return;

        contentObj.SetActive(p_show);
    }

    public void HideTopic(int p_index)
    {
        m_contentList[p_index].SetActive(false);
    }

    public void HideTopics()
    {
        foreach (var video in m_contentList)
        {
            video.SetActive(false);
        }
    }

}