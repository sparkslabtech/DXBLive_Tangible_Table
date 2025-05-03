using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjects : MonoBehaviour
{
    [SerializeField] GameObject m_cloud;
    [SerializeField] GameObject m_plane;
    [SerializeField] GameObject m_parent;
    [Space]
    [SerializeField] GameObject[] m_instantiatePositions;
    [SerializeField] GameObject[] m_lerpPositions;
    [SerializeField] Quaternion m_quaternion;
    [Space]
    [SerializeField] float m_timer = 30;
    [SerializeField] float m_lerpTimer = 3;
    [Space]
    [SerializeField] int m_index;
    [SerializeField] int m_lerpIndex;
  

    private void Start()
    {
        RandomizePositions();
        CloudMovement();
    }


    private void Update()
    {
        m_timer -= Time.deltaTime;
        if (m_timer < 0)
        {
            RandomizePositions();
            CloudMovement();
            m_timer = 30;
        }
    }

    public void CloudMovement()
    {
        RandomizePositions();
        var cloudClone=Instantiate(m_cloud, m_instantiatePositions[m_index].transform.position, m_quaternion,m_parent.transform);
        cloudClone.SetActive(true);
        cloudClone.transform.position = Vector3.Lerp(cloudClone.transform.position, m_lerpPositions[m_lerpIndex].transform.position, m_lerpTimer);
       
    }

    void RandomizePositions()
    {
        m_lerpIndex = Random.Range(0, m_lerpPositions.Length);
        m_index = Random.Range(0, m_instantiatePositions.Length);
    }
    
}
