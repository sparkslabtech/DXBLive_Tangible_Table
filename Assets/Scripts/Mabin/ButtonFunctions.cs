using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    [Header ("Buttons")]
    [SerializeField] Button[] m_childButtons;
    [SerializeField] Button m_start;
    [Header("Sprites")]
    [SerializeField] Sprite m_experiences;
    [SerializeField] Sprite m_adventure;
    [Header("GameObjects")]
    [SerializeField] GameObject[] m_markerPositions;
    [SerializeField] GameObject m_marker;
    [SerializeField] GameObject m_parent;
    [SerializeField] GameObject m_panel;

  
    public void StartButtonFunction()
    {
        if (m_start.image.sprite != m_experiences)
        {
            m_start.image.sprite = m_experiences;
            foreach (var item in m_childButtons)
            {
                item.gameObject.SetActive(true);
            }
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }

    public void ChildButtonFunctions()
    {
        foreach (var item in m_childButtons)
        {
            switch (item.gameObject.name.ToString())
            {
                case "Adventure":
                    if (m_parent.gameObject.activeInHierarchy==false && m_panel.activeInHierarchy==false)
                    {
                        m_parent.SetActive(true);
                        item.image.sprite = m_adventure;
                        foreach (var position in m_markerPositions)
                        {
                            var markerClone = Instantiate(m_marker, position.transform.position, new Quaternion(0, 0, 0, 0), m_parent.transform);
                            markerClone.SetActive(true);
                        }
                    }
                    break;
                case "Lifestyle":
                    break;
                case "Culture":
                    break;
                case "Family":
                    break;
            }
        }
    }
}