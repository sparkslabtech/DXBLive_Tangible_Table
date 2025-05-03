using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicSelector : MonoBehaviour
{
    [SerializeField] InteractablePuck interactablePuck;
    [SerializeField] float angle;
    // Start is called before the first frame update
    void Start()
    {
        interactablePuck = GetComponent<InteractablePuck>();
        interactablePuck.OnRotationSelect += OnRotationSelected;
    }

    private void OnRotationSelected(int p_indexSelected)
    {
        Debug.Log($"Rotation Selected: {p_indexSelected}");
        switch (p_indexSelected)
        {
            case 0:
                //UIVideoHandler.Instance.PlayTopic(p_indexSelected);
                UIVideoHandler.Instance.ChangeTopicLanguage(p_indexSelected);
                //PLAY ENGLISH
                break;
            case 1:
                UIVideoHandler.Instance.ChangeTopicLanguage(p_indexSelected);
                //PLAY ARABIC
                break;
            default:
                break;
        }
    }

    private void LateUpdate()
    {
        //angle = interactablePuck.GetCurrentAngle;

    }

}
