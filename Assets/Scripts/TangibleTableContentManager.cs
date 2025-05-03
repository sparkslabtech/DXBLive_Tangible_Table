using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[SerializeField]
public class SelectedMarkerTopic
{
    public int markerID;
    public int index;
}

public enum QUIDDIYA_TOPIC
{
    AQUA_ARABIA,
    DRAGONBALL,
    GAMING_AND_SPORTS,
    MERCEDES,
    PERFORMING_ARTS,
    SIX_FLAGS,
    SPEED_PARK,
    STADIUM,
}


public interface ITangibleTableContentManager
{
    void PlayVideo(int p_index, TOPIC_SIDE p_side);
    void ShowUI(int p_index, TOPIC_SIDE p_side);

    //void ShowTopic(int p_index);
    //void ShowTopic(int p_index, bool p_show);
    void HideTopic(TOPIC_SIDE p_side);
    void HideTopics();
}


public enum TOPIC_SIDE
{
    LEFT, RIGHT
}
public class TangibleTableContentManager : MonoBehaviour, ITangibleTableContentManager
{
    private static TangibleTableContentManager _instance;
    public static TangibleTableContentManager Instance => _instance;
    public QUIDDIYA_TOPIC currentTopic;

    public PuckTriggerArea leftTrigger;
    public PuckTriggerArea rightTrigger;
    //**UI handler (SID AND HAMID)
    //public bool isLeftFirst => leftTrigger.enterTime < rightTrigger.enterTime;
    //public bool isRightFirst => leftTrigger.enterTime < rightTrigger.enterTime;

    void Awake()
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
    public void HideTopic(TOPIC_SIDE p_side)
    {
        //UIHandler.Instance.HideInfographic(p_side);

        throw new System.NotImplementedException();
    }

    public void HideTopics()
    {
        throw new System.NotImplementedException();
    }

    public void PlayVideo(int p_index, TOPIC_SIDE p_side)
    {
        //UIHandler.Instance.PlayVideoOnScreen();
        throw new System.NotImplementedException();
    }

    public void ShowUI(int p_index, TOPIC_SIDE p_side)
    {
        //UIHandler.Instance.ShowInfographics( p_index,p_side);
        Debug.Log($"Triggered: {p_index} {p_side.ToString()}");
        //Debug.LogWarning($"Shown UI {QUIDDIYA_TOPIC.}");
        throw new System.NotImplementedException();
    }

}