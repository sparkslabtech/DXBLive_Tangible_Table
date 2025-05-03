using EyeFactiveMarkerManager;
using System;
using System.Collections;
using TouchScript.InputSources;
using TUIOsharp.Entities;
using UnityEngine;

namespace DisplaxTangibleTable
{
    public class DisplaxMarkerManager : MarkerManager
    {
        private protected static DisplaxMarkerManager _instance;

        public static DisplaxMarkerManager Instance => _instance;

        #region Properties
        [Header("Marker")]
        [SerializeField] protected TuioObject currentTUIOObject;
        [SerializeField] private GameObject? spawnParent;
        [SerializeField] protected Puck selectedPuck;
        [Space]
        //[SerializeField] protected GameObject[] presetMarkerObjs;
        [SerializeField] protected int[] puckIDs = new int[2];
        [Header("Debugger")]
        [SerializeField] protected float m_minDistanceToMove = 0.5f;



        [Header("Debugger")]
        public bool ObjectOnScreen = false;
        public bool showGUIDebugger = false;
        [SerializeField] protected string puckDetails;
        public Action<bool, int> OnTuioObjectDetected;
        public int lastMarkerShown;
        public float positionT;
        public float positionMultiplier = 1;
        #endregion

        #region Life Cycles
        protected override void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(this);
        }

        private void OnEnable()
        {
            m_tuioReceiver.OnObjectAddedOnScreen += OnObjectEnteredScreen;
            m_tuioReceiver.OnObjectUpdateOnScreen += OnObjectUpdateOnScreen;
            m_tuioReceiver.OnRemovedPuck += OnObjectRemovedOnScreen;
        }

        protected override void Start()
        {
#if !UNITY_EDITOR
        //showBoundaries = false;
        useMouse = false;
        showGUIDebugger = false;
#endif
            //puckIDs = new int[2];
            //puckIDs[0] = ConfigManager.GetInstance().GetValue("MARKER_ID_1");
            //puckIDs[1] = ConfigManager.GetInstance().GetValue("MARKER_ID_2");


            //showGUIDebugger = ConfigManager.GetInstance().GetBool("MARKER_SHOW_GUI");
            Init();
            PoolIndicators();
        }


        protected override void LateUpdate()
        {
            KeyboardFunctions();
            UseMouseDebug();
            ObjectOnScreen = m_tuioReceiver.IsObjectOnScreen;
            //Debug.Log($"current internal object id count : {m_tuioReceiver.currentobjectToInternalId}");

            //try
            //{
                if (currentPuckId > -1)
                    FindMarkerPosition(currentPuckId, m_tuioReceiver.GetCurrentMarkerPosition());
                else if (!m_tuioReceiver.IsObjectOnScreen)
                {
                    HideMarkers();
                }
            //}
            //catch (Exception)
            //{
                //Debug.LogError($"current puck id not found{currentPuckId}");
                //m_tuioReceiver.ClearPoints();
                //HideMarkers();
            //}
        }

        protected override void Reset()
        {
            currentPuckId = -1;
            ContentManager.Instance.HideTopics();
        }

        #endregion

        #region Events

        private void OnObjectEnteredScreen(TuioObject p_currentPuckOnScreen)
        {
            currentTUIOObject = p_currentPuckOnScreen;
            currentPuckId = p_currentPuckOnScreen.ClassId;
            //currentPuckId = CheckPuckIdIndex(p_currentPuckOnScreen.Id);
            Debug.Log($"Entered {currentPuckId}");
            OnTuioObjectDetected?.Invoke(true, currentPuckId);
        }

        private void OnObjectUpdateOnScreen(TuioObject p_currentPuckOnScreen)
        {
            currentTUIOObject = p_currentPuckOnScreen;
            markerOnScreen = m_tuioReceiver.IsObjectOnScreen;
            currentPuckId = p_currentPuckOnScreen.ClassId;
            //Debug.Log($"Object on Screen {currentPuckId}");
            puckDetails = $"Current TUIO Object Info:\n" +
                $"\n ID {currentTUIOObject.Id}" +
                $"\n rotation {currentTUIOObject.Angle}" +
                $"\n position {m_tuioReceiver.GetCurrentMarkerPosition()}" +
                $"\n Calculated 360 Angle = {GetCurrentPuckAngle()}";
        }

        private void OnObjectRemovedOnScreen(TuioObject p_currentPuckOnScreen)
        {
            //HideMarkers();
            OnTuioObjectDetected?.Invoke(false, currentPuckId);
            Debug.Log($"Removed Happened");
            //currentTUIOObject = null;
            currentPuckId = -1;
            //Reset();
        }

        #endregion

        #region Marker Position Functions

        public override void FindMarkerPosition(int p_markerID, Vector2 p_markerPos, float p_angle = 0)
        {
            ShowMarker(p_markerID, p_markerPos);
        }

        #endregion

        #region Marker Functions

        public void ShowMarker(int p_index, Vector2 p_position)
        {
            //Debug.Log($"Show Marker Received : {p_index} {p_position}");
            GameObject markerInd = m_markerIndPool[p_index];
            markerInd.SetActive(true);

            var screenWorldPosition = Camera.main.ScreenToWorldPoint(p_position);
            Vector3 markerPosition = new Vector3(-screenWorldPosition.x, screenWorldPosition.y, 3);
            //markerInd.transform.position = markerPosition;
            var distance = Vector3.Distance(markerInd.transform.position, markerPosition);
            if (distance < m_minDistanceToMove)
            {
                return;
            }
            while (Vector3.Distance(markerInd.transform.position, markerPosition) > 0.3f)
            {

                positionT = Time.deltaTime * positionMultiplier;
                markerInd.transform.position = Vector3.Lerp(markerInd.transform.position, markerPosition, 0.9f);
                Debug.Log($"position distance {Vector3.Distance(markerInd.transform.position, markerPosition)}");
            }

        }

        public override IEnumerator HideMarker(int p_index)
        {
            Debug.Log($"Coroutine Started");
            coroutineRunning = true;
            yield return new WaitForSeconds(markerTimeDisappear);
            Debug.Log($"Coroutine Ended");

            m_markerIndPool[p_index].SetActive(false);
            //ContentManager.Instance.HideTopic(p_index);
            Debug.Log($"Marker {p_index} Hidden ");
            Debug.Log($"Marker {m_markerIndPool[p_index].name} Hidden ");
            coroutineRunning = false;
        }

        public void HideMarkers()
        {
            //TopicVideoManager.GetInstance.ShowScreenSaver();
            //TopicVideoManager.GetInstance.SetSelected(false);
            foreach (var marker in m_markerIndPool)
            {
                marker.SetActive(false);
            }
        }

        #endregion

        #region Debug

        private void UseMouseDebug()
        {
            if (useMouse)
            {
                if (Input.GetMouseButton(0))
                {
                    FindMarkerPosition(debugMarkerIndex, Input.mousePosition, 1);
                }
            }
        }

        private void KeyboardFunctions()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
                m_tuioReceiver.ClearPoints();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                debugMarkerIndex = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                debugMarkerIndex = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                debugMarkerIndex = 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                debugMarkerIndex = 4;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HideMarkers();
            }

        }

        void OnGUI()
        {
            if (!showGUIDebugger)
            {
                return;
            }
            GUI.color = Color.red;
            GUI.Label(new Rect(_screenX / 2, 150, 1000, 1000), $"{puckDetails}");
        }

        #endregion

        public int CheckPuckIdIndex(int p_currentPuckOnScreen)
        {
            currentPuckId = GetPuckIDShort(p_currentPuckOnScreen);
            for (int i = 0; i < puckIDs.Length; i++)
            {
                if (currentPuckId == puckIDs[i])
                {
                    return i;
                }
            }
            Debug.LogError($"Puck not listed");
            return -1;
        }

        public int GetPuckIDShort(int p_id)
        {
            var puckIDString = p_id.ToString().Substring(0, 3);
            return currentPuckId = int.Parse(puckIDString);
        }

        public float GetCurrentPuckAngle()
        {
            if (currentTUIOObject != null)
            {
                var i = currentTUIOObject.Angle / 6.5f; // 6.5 => Max Rotation from the PUCK
                return Mathf.Lerp(0, 359, i);
            }
            //Debug.LogError($"Did not find lerped Angle");
            return -1;
        }


    }
}
