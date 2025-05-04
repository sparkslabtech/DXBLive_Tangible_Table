using EyeFactiveMarkerManager;
using UnityEngine;

public class MarkerTriggerDXBLive : PuckTriggerArea
{
    //[SerializeField] DXBLiveTopicHandler topicHandler;
    //[SerializeField] TableTopicHandler TableTopicHandler;
    [SerializeField] UIVideoHandler mainUIHandler;

    // Start is called before the first frame update
    void Start()
    {
        currentPuckPlaced = 0;
    }

    protected override void OnPuckEnterBehaviour(Collider2D collision)
    {
        base.OnPuckEnterBehaviour(collision);
        var intPuck = collision.gameObject.GetComponent<InteractablePuck>();
        intPuck.SetInBoundary(true);
        intPuck.isSelectionEnabled = true;
    }

    protected override void OnPuckExitBehaviour(Collider2D collision)
    {
        base.OnPuckExitBehaviour(collision);
        var intPuck = collision.gameObject.GetComponent<InteractablePuck>();
        intPuck.isSelectionEnabled = false;
        intPuck.isInsideBoundary = false;

        mainUIHandler.StopTopics();

    }

    protected override void OnPuckStayBehaviour(Collider2D collision)
    {
        base.OnPuckStayBehaviour(collision);

        var intPuck = collision.gameObject.GetComponent<InteractablePuck>();
        intPuck.SetInBoundary(true);
        intPuck.isSelectionEnabled = true;

        int topicHandlerIndex = MarkerManager.GetMarkerIDToIndex(currentPuckPlaced);
        mainUIHandler.PlayTopic(topicHandlerIndex);

        //topicHandler.PlayTopic(topicHandlerIndex);
        //TableTopicHandler.PlayTopic(topicHandlerIndex);

        /////////////////////////////////////////////////////
        //Debug.Log($"Topic Index: {topicHandlerIndex}");
        //Debug.Log("Topic Called");
        //Debug.Log("Topic Table Called");


        //topicHandler.PlayTopic(MarkerManager.GetMarkerIDToIndex(currentPuckPlaced));
        //Debug.Log("Topic Called");
        //TableTopicHandler.PlayTopic(MarkerManager.GetMarkerIDToIndex(currentPuckPlaced));
        //Debug.Log("Topic Table Called");
    }


}
