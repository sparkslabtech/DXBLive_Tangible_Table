using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DisplaxTangibleTable;
using EyeFactiveMarkerManager;

public class InteractablePuck : MonoBehaviour
{
    //[SerializeField] ImageLoader m_topicImageManager;
    [Header("Physical Details")]
    [SerializeField] int puckID = 1;
    [SerializeField] float initAngle = 0;

    [Header("Selected Indexes")]
    [SerializeField] int m_defaultSelectedIndex = 0;
    [SerializeField] int m_currentSelectedIndex = -1;
    [SerializeField] int m_selectingIndex = -1;
    [SerializeField] int m_markedIndex = -1;

    [Header("GameObjects")]
    [SerializeField] GameObject m_selectionTopicParent;
    [Space]

    [Header("Topic Selection")]
    [SerializeField] GameObject[] m_topicButtons;
    [Space]

    [Header("Angles")]
    [Space]
    [SerializeField] float rotationSensitivity = 1;
    [SerializeField] bool useCustomAngle = false;
    [SerializeField] List<int> m_topicAngles;
    [SerializeField] float SetDefaultLanguage = 90;
    [Space]
    [SerializeField] Color m_fillColor = Color.yellow;
    //[SerializeField] Color m_topicIdleColour = Color.white;
    //[SerializeField] Color m_topicSelectedColour = Color.blue;
    [Space]

    [Header("Timer")]
    [SerializeField] Image m_markerFill;
    [SerializeField] float m_currentTime;
    [SerializeField] float m_selectionMaxTime = 2;

    [Header("Debugger")]
    public bool DebugMode = false;
    public bool showAngleVisualizer = false;
    public bool reverseAngles = false;
    [SerializeField] GameObject angleVisualizer;
    [Range(0f, 359)]
    public float m_visualizerAngle = 0;
    [SerializeField] private float m_angle = 0;
    public int nearestIndexDebug = 0;

    public Action<int> OnRotationSelect;

    public bool isInsideBoundary = false;
    public bool isSelectionEnabled = false;
    //public float GetCurrentAngle => m_visualizerAngle;
    public float GetCurrentAngle => m_angle;
    public int GetPuckID => puckID;
    public void SetPuckID(int p_classID)
    {
        puckID = p_classID;
    }

    #region Life Cycles
    private void OnEnable()
    {
        //DisplaxMarkerManager.Instance.OnTuioObjectDetected += OnPuckDetected;
        initAngle = MarkerManager.Instance.GetCurrentPuckAngle();

        //MarkerManager.Instance.OnTuioObjectDetected += OnPuckDetected;
        ResetValues();
        //angleDebugger = 0;
    }

    void Start()
    {
#if UNITY_EDITOR
        showAngleVisualizer = true;
#else
        showAngleVisualizer = false;
#endif
        rotationSensitivity = ConfigManager.Instance.GetFloatValue("PUCK_ROTATION_SENSITITIVITY");
        ResetValues();
    }

    void ResetValues()
    {
        isInsideBoundary = false;
        m_currentTime = 0;
        m_currentSelectedIndex = -1;
        m_selectingIndex = -1;
        m_markedIndex = m_defaultSelectedIndex;
    }

    void FixedUpdate()
    {
        //    FindClosestAngle();//FIND ANGLE
        //    //MarkerSelect();//SELECTS TOPIC ANGLE
        //    SetSelectedRotation();//SELECT ROTATION
        if (isSelectionEnabled)
        {
            FindClosestAngle();//FIND ANGLE
            SetSelectedRotation();//SELECT ROTATION
        }
    }

    private void OnDisable()
    {
        ResetValues();
        //TopicVideoManager.GetInstance.ShowScreenSaver();
    }

    #endregion
    private void OnPuckDetected()
    {
        //initAngle = MarkerManager.Instance.GetCurrentPuckAngle();
    }

    public void SetInBoundary(bool p_isInside)
    {
        isInsideBoundary = p_isInside;
    }

    void MarkerSelect()
    {
        bool isSelected = m_currentSelectedIndex == m_markedIndex;
        if (!isSelected)
        {
            //m_selectingIndex = m_markedIndex;
            m_currentTime += Time.deltaTime;
            HighlightTopic(m_markedIndex);
            Debug.Log($"Highlighted topic: {m_markedIndex}");

            if (m_currentTime >= m_selectionMaxTime)
            {
                m_currentTime = m_selectionMaxTime;
                //m_markerFill.fillAmount = (m_currentTime / m_selectionMaxTime);
                SelectTopic(m_markedIndex);
                m_currentTime = 0;
            }
        }
        m_markerFill.fillAmount = (m_currentTime / m_selectionMaxTime);
    }

    private void Reset()
    {
        m_currentTime = 0;
    }

    #region Marker Rotation

    void SetSelectedRotation()
    {
        if (DebugMode)
            angleVisualizer.transform.rotation = Quaternion.Euler(0, 0, 360 - m_visualizerAngle);
        else
        {
            if (initAngle == -1)
            {
                initAngle = DisplaxMarkerManager.Instance.GetCurrentPuckAngle();
            }
            //Debug.Log($"init Calculated rotation : 360-{m_topicAngles[m_defaultSelectedIndex]}- {initAngle} - {DisplaxMarkerManager.Instance.GetCurrentPuckAngle()}");
            //m_visualizerAngle = 360 - (m_topicAngles[m_defaultSelectedIndex] - (initAngle - DisplaxMarkerManager.Instance.GetCurrentPuckAngle()));
            //Debug.Log($"init angle:{initAngle}");
            //Debug.Log($"current angle:{MarkerManager.Instance.GetCurrentPuckAngle()}");

            //m_visualizerAngle = 360 - (m_topicAngles[m_defaultSelectedIndex] - (initAngle - MarkerManager.Instance.GetCurrentPuckAngle()) - 90);
            //m_visualizerAngle = 360 - (initAngle - MarkerManager.Instance.GetCurrentPuckAngle()) - 90;
            m_visualizerAngle = (initAngle - MarkerManager.Instance.GetCurrentPuckAngle()) - SetDefaultLanguage;
            //Debug.LogWarning($"Calculated Angle ({m_visualizerAngle}*{rotationSensitivity}) = {m_visualizerAngle * rotationSensitivity}");
            m_visualizerAngle = m_visualizerAngle * rotationSensitivity;
            angleVisualizer.transform.rotation = Quaternion.Euler(0, 0, m_visualizerAngle);
        }
    }

    void FindClosestAngle()
    {
        //Get Angle in Euler
        //List<int> list = m_topicAngles.ToList();
        List<int> list = m_topicAngles;

        //if (!angleVisualizer.activeInHierarchy)
        //{
        //    angleVisualizer.gameObject.SetActive(false);
        //}
        try
        {

            var angle = angleVisualizer.transform.rotation.eulerAngles.z;
            var nearest = m_topicAngles.OrderBy(x => Math.Abs((long)x - angle)).First();
            //Debug.Log($"nearest {nearest} to current Angle : {angle}");
            var nearestIndex = list.FindIndex(x => x == nearest);

            //DEBUG start
            m_angle = angle;
            nearestIndexDebug = nearestIndex;
            OnRotationSelect?.Invoke(nearestIndex);
            //Debug end 

            switch (puckID)
            {
                case 0:
                    m_markedIndex = nearestIndex - 1;
                    break;
                case 1:
                    m_markedIndex = nearestIndex;
                    break;
                default:
                    break;
            }


            //if (useCustomAngle && IsPointingAtStartUsingCustomAngle(angle))
            //    return;
            if (IsPointingAtStart(angle))
                return;
            else if (m_markedIndex <= -1)
            //if (m_markedIndex <= -1)
            {
                m_markedIndex = 0;
            }
            else if (m_markedIndex == m_topicButtons.Length)
            {
                m_markedIndex = m_currentSelectedIndex - 1;
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    bool IsPointingAtStart(float p_angle)
    {
        int totalChildCount = m_selectionTopicParent.transform.childCount;

        //i.e. 360/8 = 45
        //a = 22.5
        //b = 360-22.5 = 337.5

        //Get Angle
        float distancePerAngle = 360 / totalChildCount;
        float halfDistance = distancePerAngle / 2;
        bool isPointingAtFirst = p_angle < halfDistance || p_angle > (360 - halfDistance);

        if (isPointingAtFirst)
            m_markedIndex = totalChildCount - 1;

        return isPointingAtFirst;
    }

    bool IsPointingAtStartUsingCustomAngle(float p_angle)
    {
        int totalChildCount = m_selectionTopicParent.transform.childCount;

        //i.e. 360/8 = 45
        //a = 22.5
        //b = 360-22.5 = 337.5

        float leftDiff, rightDiff;
        leftDiff = m_topicAngles[m_topicAngles.Count - 1] / 2;
        rightDiff = m_topicAngles[0] / 2;
        //Debug.Log($"New Diffs : leftDiff {leftDiff} rightDiff{rightDiff}");

        //Get Angle
        //float distancePerAngle = 360 / totalChildCount;
        //float halfDistance = distancePerAngle / 2;
        bool isPointingAtFirst = p_angle < leftDiff || p_angle > (360 - rightDiff);

        if (isPointingAtFirst)
            m_markedIndex = totalChildCount - 1;

        return isPointingAtFirst;
    }
    #endregion

    #region Topic Selection

    void SelectTopic(int p_index)
    {
        m_currentSelectedIndex = p_index;
        var selectedObj = m_topicButtons[m_currentSelectedIndex];

        int index = m_topicButtons.Length - m_currentSelectedIndex - 1;

        Deselect();
        //selectedObj.GetComponent<RawImage>().texture = m_topicImageManager.GetButtonImage(index, ButtonState.SELECTED);
        TopicVideoManager.GetInstance.PlayTopicVideo(puckID, m_currentSelectedIndex);
    }

    void HighlightTopic(int p_index)
    {
        m_selectingIndex = p_index;
        var selectedObj = m_topicButtons[m_selectingIndex];

        Deselect();
        int index = m_topicButtons.Length - m_selectingIndex - 1;
        //selectedObj.GetComponent<RawImage>().texture = m_topicImageManager.GetButtonImage(index, ButtonState.HIGHLIGHTED);
    }

    void Deselect()
    {
        //m_topicImageManager.SetButtonsTo(ButtonState.NORMAL);
    }

    #endregion

    #region Tools

    [ContextMenu("Set Name Index")]
    public void SetTopicIndexNames()
    {
        GetChildren();

        foreach (var topicButton in m_topicButtons)
        {
            topicButton.name += $" {topicButton.transform.GetSiblingIndex()}";
        }
    }

    public void GetChildren()
    {
        List<GameObject> childrenFound = new List<GameObject>();
        for (int childIndex = 0; childIndex < m_selectionTopicParent.transform.childCount; childIndex++)
        {
            childrenFound.Add(m_selectionTopicParent.transform.GetChild(childIndex).gameObject);
        }
        childrenFound.Reverse();
        m_topicButtons = childrenFound.ToArray();
    }

    public void GetAngles()
    {
        if (useCustomAngle)
            return;

        int diff = 360 / m_selectionTopicParent.transform.childCount;
        List<int> childrenFound = new List<int>();
        for (int childIndex = 0; childIndex < m_selectionTopicParent.transform.childCount; childIndex++)
        {
            childrenFound.Add(childIndex * diff);
        }
        childrenFound.Reverse();
        m_topicAngles = childrenFound;
    }

    public void ReverseAngles()
    {
        if (useCustomAngle && reverseAngles)//Only when its custom
            m_topicAngles.Reverse();
    }
    #endregion

}