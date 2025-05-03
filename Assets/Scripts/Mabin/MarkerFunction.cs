using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MarkerFunction : MonoBehaviour
{
    [SerializeField] GameObject[] m_toEnable;
    [SerializeField] GameObject[] m_toDisable;

    private void OnMouseDown()
    {
        foreach (var item in m_toEnable)
        {
            item.SetActive(true);
        }
        foreach (var item in m_toDisable)
        {
            item.SetActive(false);
        }
    }

}
