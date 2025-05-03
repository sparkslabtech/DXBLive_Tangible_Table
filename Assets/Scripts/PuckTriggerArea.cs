using System;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class PuckTriggerArea : MonoBehaviour
{
    [SerializeField] protected int currentPuckPlaced = 0;
    //[SerializeField] protected TOPIC_SIDE side = TOPIC_SIDE.LEFT;
    [SerializeField] protected string m_puckTag = "Puck";
    [SerializeField] protected DateTime enterTime;

    void Start()
    {
        currentPuckPlaced = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag.Equals(m_puckTag))
        {
            //Debug.LogWarning($"Entered {collision.gameObject.name}");
            OnPuckEnterBehaviour(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag.Equals(m_puckTag))
        {
            //Debug.LogWarning($"Exited {collision.gameObject.name}");
            OnPuckStayBehaviour(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag.Equals(m_puckTag))
        {
            //Debug.LogWarning($"Exited {collision.gameObject.name}");
        }
    }

    protected virtual void OnPuckEnterBehaviour(Collider2D collision)
    {
        var gameObject = collision.gameObject;
        var puckDetails = gameObject.GetComponent<InteractablePuck>();
        currentPuckPlaced = puckDetails.GetPuckID;
        enterTime = DateTime.Now;

        Debug.LogAssertion($"Puck Entered {puckDetails.GetPuckID}");
    }

    protected virtual void OnPuckStayBehaviour(Collider2D collision)
    {
        var gameObject = collision.gameObject;
        var puckDetails = gameObject.GetComponent<InteractablePuck>();
        currentPuckPlaced = puckDetails.GetPuckID;
        enterTime = DateTime.Now;

        Debug.LogAssertion($"Puck Stay {puckDetails.GetPuckID}");
    }

    protected virtual void OnPuckExitBehaviour(Collider2D collision)
    {
        var gameObject = collision.gameObject;
        var puckDetails = gameObject.GetComponent<InteractablePuck>();
        currentPuckPlaced = 0;

        Debug.LogAssertion($"Puck Exited {puckDetails.GetPuckID}");
    }

}
