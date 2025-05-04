using System.Collections;
using UnityEngine;
using TouchScript.InputSources;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;
using TUIOsharp.Entities;


//MARKER BASE CLASS DO NOT TOUCH!
//Should only handle getting TUIO Objects from TUIOInput Script
//Any data processing should be on a different script



namespace EyeFactiveMarkerManager
{

    public enum DISPLAX_PASSIVE_PUCK_ID
    {
        Puck201 = 1,
        Puck202,
        Puck203,
        Puck204,
        Puck301,
        Puck302,
    }

    public class MarkerManager : MonoBehaviour
    {
        private protected static MarkerManager _instance;
        public static MarkerManager Instance => _instance;


        #region Properties

        [Header("Objects")]
        public TuioInput m_tuioReceiver;

        [Header("Marker")]
        [SerializeField] protected int[] markersTracked;
        [SerializeField] protected static int[] markerIDs = { 201, 202, 203, 204, 301, 302 };
        [SerializeField] protected int currentPuckId = 0;
        [Space]
        private string currentMarker;
        private string currentPosition;
        private string currentRotation;
        [Space]
        public string currentObjInfo;

        [Header("Indicator")]
        [SerializeField] protected GameObject m_markerIndicatorPrefab;
        [SerializeField] protected GameObject[] m_markerIndPool;

        [SerializeField] protected int m_markerMax = 4;
        [SerializeField] protected float markerTimeDisappear = 1;

        [Header("Marker Boundaries")]
        protected int _screenX;
        protected int _screenY;

        [Header("Debugger")]
        private bool showBoundaries = false;
        [SerializeField] protected bool useMouse = false;
        [SerializeField] protected bool markerOnScreen;
        [SerializeField] protected bool isMarkerCurrentlyPlaced;
        [SerializeField] protected bool coroutineRunning = false;
        [SerializeField] protected int debugMarkerIndex;
        #endregion

        public Action OnNoPucksDetected;
        public Action OnTuioObjectDetected;


        public float GetCurrentPuckAngle()
        {
            if (m_tuioReceiver.currentTUIObject != null)
            {
                var i = m_tuioReceiver.currentTUIObject.Angle / 6.5f; // 6.5 => Max Rotation from the PUCK
                return Mathf.Lerp(0, 360, i);
            }
            //Debug.LogError($"Did not find lerped Angle");
            return -1;
        }

        #region Getters
        public bool ExistPuck(int p_index)
        {
            foreach (var item in markersTracked)
            {
                if (item == p_index)
                {
                    return true;
                }
            }
            return false;
        }
        public int GetCurrentPuckID() => currentPuckId;
        public string GetCurrentMarker() => currentMarker;
        public string GetCurrentPosition() => currentPosition;
        public string GetCurrentRotation() => currentRotation;
        public float GetCurrentRotationAngle() => m_tuioReceiver.currentTUIObject.Angle;
        #endregion

        #region LifeCycles

        protected virtual void Awake()
        {
            Debug.Log($"Awake Happened");
            if (_instance == null)
                _instance = this;
            else
                Destroy(this);
        }

        protected virtual void Start()
        {
#if !UNITY_EDITOR
        showBoundaries = false;
        useMouse = false;
#endif

            Debug.Log($"Start Happened");
            m_tuioReceiver.OnObjectUpdateOnScreen += OnPuckObjectDetected;
            Init();
            PoolIndicators();
        }

        private void OnPuckObjectDetected(TuioObject @object)
        {
            OnTuioObjectDetected?.Invoke();
            //throw new NotImplementedExceptkion();
        }

        protected virtual void LateUpdate()
        {
            KeyboardFunctions();
            UseMouseDebug();

            if (!useMouse)
            {
                if (m_tuioReceiver.CurrentTotalObjs > 0)
                {
                    foreach (var item in m_tuioReceiver.GetObjsList)
                    {
                        FindMarkerPosition(item.Value.ObjectId, item.Value.Position);
                    }
                }
                else if (!AllMarkersHidden())
                    HideAllMarkers();

                currentObjInfo = m_tuioReceiver.GetObjsAvailableMessage();
                TrackObjectOnScreen();
            }
            else
            {
                //if (!AllMarkersHidden())
                //    HideAllMarkers();
                if (!Input.GetMouseButton(0))
                {
                    //FindMarkerPosition(debugMarkerIndex, Input.mousePosition);
                    if (!AllMarkersHidden())
                        HideAllMarkers();
                }
            }


        }

        #endregion

        #region Marker Handler
        public bool AllMarkersHidden()
        {
            foreach (var item in m_markerIndPool)
            {
                if (item.activeInHierarchy)
                {
                    return false;
                }
            }
            return true;
        }

        private void HideAllMarkers()
        {
            foreach (var item in m_markerIndPool)
            {
                item.gameObject.SetActive(false);
            }
            OnNoPucksDetected?.Invoke();

        }

        public void TrackObjectOnScreen()
        {
            if (m_tuioReceiver.CurrentTotalObjs == markersTracked.Length)
                return;
            markersTracked = new int[m_tuioReceiver.CurrentTotalObjs];
            int i = 0;
            foreach (var item in m_tuioReceiver.GetObjsList)
            {
                markersTracked[i] = item.Value.ObjectId;
                i++;

                //FindMarkerPosition(item.Value.ObjectId, item.Value.Position);
            }

        }
        #endregion

        protected virtual void Reset()
        {
            currentPuckId = 0;
            currentPosition = "";
            ContentManager.Instance.HideTopics();
        }

        #region Init

        protected void Init()
        {
            coroutineRunning = false;
            markerOnScreen = false;
            _screenX = Screen.width;
            _screenY = Screen.height;
        }

        protected void PoolIndicators()
        {
            m_markerIndPool = new GameObject[m_markerMax];
            for (int markerIndex = 0; markerIndex < m_markerMax; markerIndex++)
            {
                var newMarker = Instantiate(m_markerIndicatorPrefab, null);
                m_markerIndPool[markerIndex] = newMarker;
            }
        }

        #endregion

        #region Marker Position Functions

        public virtual void FindMarkerPosition(int p_markerID, Vector2 p_markerPos, float p_angle = 0)
        {
            //int index = p_markerID - 1;

            currentMarker = $"Current Marker = {p_markerID}";
            currentPosition = $"Current Position = x: {p_markerPos.x} y: {p_markerPos.y}";
            currentRotation = $"Current Rotation Angle= x: {p_angle}";
            currentPuckId = p_markerID;



            ShowIndicator(p_markerID, p_markerPos);
        }

        #endregion

        #region Indicator Functions

        public virtual void ShowIndicator(int p_index, Vector2 p_position)
        {
            int markerIndex = GetMarkerIDToIndex(p_index);

            GameObject markerInd = m_markerIndPool[markerIndex];
            markerInd.SetActive(true);
            markerInd.GetComponent<InteractablePuck>().SetPuckID(p_index);
            markerInd.transform.position = Camera.main.ScreenToWorldPoint(p_position);
        }


        public virtual IEnumerator HideMarker(int p_index)
        {
            coroutineRunning = true;

            yield return new WaitForSeconds(markerTimeDisappear);
            m_markerIndPool[p_index].SetActive(false);
            ContentManager.Instance.HideTopic(p_index);
            Debug.Log("Marker Hidden");
            coroutineRunning = false;
        }


        public async void WaitToEnd(float p_duration)
        {
            Debug.Log("Wait To End Started");
            var end = Time.time + p_duration;

            while (Time.time < end)
            {
                await Task.Yield();
            }
            Debug.Log("Wait To End Ended");
        }

        #endregion

        #region Debug

        private void UseMouseDebug()
        {
            if (useMouse)
            {
                if (Input.GetMouseButton(0))
                {
                    FindMarkerPosition(debugMarkerIndex, Input.mousePosition);
                }
            }
        }

        private void KeyboardFunctions()
        {
            //Fix this
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                debugMarkerIndex = 201;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                debugMarkerIndex = 202;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                debugMarkerIndex = 203;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                debugMarkerIndex = 204;
            }
        }

        public void EnableMousePuckSimulation(bool p_useMouse)
        {
            useMouse = p_useMouse;

            if (p_useMouse)
            {
                debugMarkerIndex = (int)DISPLAX_PASSIVE_PUCK_ID.Puck201;
            }
        }

        #endregion

        #region Static Fucntions

        public static int GetMarkerIDToIndex(int p_classID)
        {
            int index = 0;
            foreach (var item in markerIDs)
            {
                if (item == p_classID)
                {
                    return index;
                }
                index++;
            }
            Debug.LogError($"Could not find id: {p_classID}");
            return -1;
        }

        public static int GetMarkerIDToIndex(DISPLAX_PASSIVE_PUCK_ID p_displaxPuckID)
        {
            int index = 0;
            foreach (var item in markerIDs)
            {
                if (item == int.Parse(p_displaxPuckID.ToString()))
                {
                    return index;
                }
                index++;
            }
            Debug.LogError($"Could not find id: {p_displaxPuckID}");
            return -1;
        }
        #endregion
    }
}