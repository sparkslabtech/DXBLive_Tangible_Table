using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiglightLanguageBtn : MonoBehaviour
{
    [SerializeField] InteractablePuck interactablePuck;
    [SerializeField] Image engBtn, arbBtn;
    [SerializeField] Sprite engSpriteInactive, arbSpriteInactive;
    [SerializeField] Sprite engSpriteActive, arbSpriteActive;

    // Start is called before the first frame update
    void Start()
    {
        interactablePuck = GetComponent<InteractablePuck>();
        interactablePuck.OnRotationSelect += OnRotationSelected;
    }

    private void OnRotationSelected(int p_indexSelected)
    {
        Debug.Log($"Btn highlighted: {p_indexSelected}");
        switch (p_indexSelected)
        {
            case 0:
                //UIVideoHandler.Instance.PlayTopic(p_indexSelected);
                //UIVideoHandler.Instance.ChangeTopicLanguage(p_indexSelected);
                engBtn.sprite = engSpriteActive;
                arbBtn.sprite = arbSpriteInactive;

                //PLAY ENGLISH
                break;
            case 1:
                //UIVideoHandler.Instance.ChangeTopicLanguage(p_indexSelected);
                //PLAY ARABIC
                arbBtn.sprite = arbSpriteActive;
                engBtn.sprite = engSpriteInactive;
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
