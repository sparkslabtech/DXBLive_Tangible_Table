using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public enum ButtonState
{
    NORMAL,
    SELECTED,
    HIGHLIGHTED
}

public class ButtonImageManager : MonoBehaviour
{
    [SerializeField] GameObject m_buttonParent;

    [SerializeField] Sprite[] m_buttonImageNormal;
    [SerializeField] Sprite[] m_buttonImageSelected;
    [SerializeField] Sprite[] m_buttonImageHighLighted;

    void Start()
    {

        //ReverseImages();
        SetButtonsToNormal();
    }

    [ContextMenu("Reverse")]
    public void ReverseImages()
    {
        m_buttonImageNormal = m_buttonImageNormal.Reverse().ToArray();
        m_buttonImageSelected = m_buttonImageSelected.Reverse().ToArray();
        m_buttonImageHighLighted = m_buttonImageHighLighted.Reverse().ToArray();
    }

    [ContextMenu("Set Images to Normal")]
    public void SetButtonsToNormal()
    {
        SetButtonsTo(ButtonState.NORMAL);
    }

    public Sprite GetButtonImage(int p_index, ButtonState p_state = ButtonState.NORMAL)
    {
        Sprite[] buttonImages = GetSpriteType(p_state);
        return buttonImages[p_index];
    }

    public void SetButtonsTo(ButtonState p_state = ButtonState.NORMAL)
    {
        Sprite[] buttonImages = GetSpriteType(p_state);

        for (int childIndex = 0; childIndex < m_buttonParent.transform.childCount; childIndex++)
        {
            Image childImage = m_buttonParent.transform.GetChild(childIndex).GetComponent<Image>();
            childImage.sprite = buttonImages[childIndex];
        }
    }

    public void SetButtonTo(int p_index, ButtonState p_state = ButtonState.NORMAL)
    {
        Sprite[] buttonImages = GetSpriteType(p_state);
        Image childImage = m_buttonParent.transform.GetChild(p_index).GetComponent<Image>();
        childImage.sprite = buttonImages[p_index];
    }

    public Sprite[] GetSpriteType(ButtonState p_state = ButtonState.NORMAL)
    {
        switch (p_state)
        {
            case ButtonState.SELECTED:
                return m_buttonImageSelected;

            case ButtonState.HIGHLIGHTED:
                return m_buttonImageHighLighted;

            default:
                return m_buttonImageNormal;
        }
    }

}