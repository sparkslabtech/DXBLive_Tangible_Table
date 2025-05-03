using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EyeFactiveMarkerManager
{

    public class TagPuck
    {
        public int index;
        public DateTime enterTime;
    } 

    public class MarkerCornerHandler : MarkerManager
    {
        [Header("Tag Handler")]
        public TagPuck firstPuck;
        public TagPuck secondPuck;

        [Header("Indicator")]
        [SerializeField] private GameObject[] m_markerBoundaryObjs;
        [SerializeField] private Color[] m_markerColour;

        [Header("Marker Boundaries")]
        [SerializeField] protected bool showBoundaries = true;
        [SerializeField] int m_areaSizeX = 300;
        [SerializeField] int m_areaSizeY = 300;

        [Header("Boundary Offset")]
        [SerializeField] private Vector2 boundaryOffset = new Vector2(20, 20); // New! X = horizontal offset, Y = vertical offset


        void OnGUI()
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(_screenX / 2, 150, 180, 20), $"{GetCurrentMarker()}");
            GUI.Label(new Rect(_screenX / 2, 180, 300, 20), $"{GetCurrentPosition()}");
            GUI.Label(new Rect(_screenX / 2, 220, 300, 20), $"{GetCurrentRotation()}");
            ShowBoundaries();
            //SetBoundaryColours();
        }

        void SetBoundaryColours()
        {
            for (int markerIndex = 0; markerIndex < m_markerMax; markerIndex++)
            {
                m_markerBoundaryObjs[markerIndex].GetComponent<Image>().color = GetBoundaryColor(markerIndex);
            }
        }

        #region Boundary

        private Color GetBoundaryColor(int p_markerIndex)
        {
            var markerColor = m_markerColour[p_markerIndex];
            markerColor.a = 0.5f;
            return markerColor;
        }

        void SelectBoundary(int p_index)
        {
            m_markerBoundaryObjs[p_index].SetActive(true);
        }

        void ShowAllBoundary(bool p_show)
        {
            foreach (var boundary in m_markerBoundaryObjs)
            {
                boundary.SetActive(p_show);
            }
        }

        public override void FindMarkerPosition(int p_markerID, Vector2 p_markerPos, float p_angle = 0)
        {
            int index = p_markerID - 1;
            bool isInsideBoundary = IsMarkerInBoundary(index, p_markerPos);
            base.FindMarkerPosition(p_markerID, p_markerPos, p_angle);
            ContentManager.Instance.ShowTopic(index, isInsideBoundary);
            ShowAllBoundary(false);
            SelectBoundary(index);
        }

        public override IEnumerator HideMarker(int p_index)
        {
            ShowAllBoundary(true);
            return base.HideMarker(p_index);
        }

        protected bool IsMarkerInBoundary(int p_markerIndex, Vector2 p_markerPos)
        {
            bool isInside = false;

            switch (p_markerIndex)
            {
                case 0:
                    AreaPointCalculator.Point[] polygon1 = {
                new AreaPointCalculator.Point(0 + (int)boundaryOffset.x, _screenY - (int)boundaryOffset.y),
                new AreaPointCalculator.Point(m_areaSizeX - (int)boundaryOffset.x, _screenY - (int)boundaryOffset.y),
                new AreaPointCalculator.Point(m_areaSizeX - (int)boundaryOffset.x, _screenY - m_areaSizeY + (int)boundaryOffset.y),
                new AreaPointCalculator.Point(0 + (int)boundaryOffset.x, _screenY - m_areaSizeY + (int)boundaryOffset.y)};
                    isInside = AreaPointCalculator.isInside(polygon1, 4, new AreaPointCalculator.Point((int)p_markerPos.x, (int)p_markerPos.y));
                    break;

                case 1:
                    AreaPointCalculator.Point[] polygon2 = {
                new AreaPointCalculator.Point(_screenX - m_areaSizeX + (int)boundaryOffset.x, _screenY - (int)boundaryOffset.y),
                new AreaPointCalculator.Point(_screenX - (int)boundaryOffset.x, _screenY - (int)boundaryOffset.y),
                new AreaPointCalculator.Point(_screenX - (int)boundaryOffset.x, _screenY - m_areaSizeY + (int)boundaryOffset.y),
                new AreaPointCalculator.Point(_screenX - m_areaSizeX + (int)boundaryOffset.x, _screenY - m_areaSizeY + (int)boundaryOffset.y)};
                    isInside = AreaPointCalculator.isInside(polygon2, 4, new AreaPointCalculator.Point((int)p_markerPos.x, (int)p_markerPos.y));
                    break;

                case 2:
                    AreaPointCalculator.Point[] polygon3 = {
                new AreaPointCalculator.Point(0 + (int)boundaryOffset.x, 0 + (int)boundaryOffset.y),
                new AreaPointCalculator.Point(m_areaSizeX - (int)boundaryOffset.x, 0 + (int)boundaryOffset.y),
                new AreaPointCalculator.Point(m_areaSizeX - (int)boundaryOffset.x, m_areaSizeY - (int)boundaryOffset.y),
                new AreaPointCalculator.Point(0 + (int)boundaryOffset.x, m_areaSizeY - (int)boundaryOffset.y)};
                    isInside = AreaPointCalculator.isInside(polygon3, 4, new AreaPointCalculator.Point((int)p_markerPos.x, (int)p_markerPos.y));
                    break;

                case 3:
                    AreaPointCalculator.Point[] polygon4 = {
                new AreaPointCalculator.Point(_screenX - m_areaSizeX + (int)boundaryOffset.x, 0 + (int)boundaryOffset.y),
                new AreaPointCalculator.Point(_screenX - (int)boundaryOffset.x, 0 + (int)boundaryOffset.y),
                new AreaPointCalculator.Point(_screenX - (int)boundaryOffset.x, m_areaSizeY - (int)boundaryOffset.y),
                new AreaPointCalculator.Point(_screenX - m_areaSizeX + (int)boundaryOffset.x, m_areaSizeY - (int)boundaryOffset.y)};
                    isInside = AreaPointCalculator.isInside(polygon4, 4, new AreaPointCalculator.Point((int)p_markerPos.x, (int)p_markerPos.y));
                    break;
            }

            return isInside;
        }

        public void ShowBoundaries()
        {
            if (!showBoundaries)
                return;

            //Boundaries

            //Marker 1 Area
            GUI.Box(new Rect(0, 0, m_areaSizeX, m_areaSizeY), "Marker 1 Area");

            //Marker 2 Area
            GUI.Box(new Rect(_screenX - m_areaSizeX, 0, m_areaSizeX, m_areaSizeY), "Marker 2 Area");

            ////Marker 3 Area
            GUI.Box(new Rect(0, _screenY - m_areaSizeY, m_areaSizeX, m_areaSizeY), "Marker 3 Area");

            ////Marker 4 Area
            GUI.Box(new Rect(_screenX - m_areaSizeX, _screenY - m_areaSizeY, m_areaSizeX, m_areaSizeY), "Marker 4 Area");
        }

        #endregion
    }
}