using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Vector3 initPosition = Vector3.zero;
    [Header("Animation")]
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float speed;
    [SerializeField] float animTime = 1;

    [Header("Debugger")]
    [SerializeField] float debugSphereSize = 0.1f;
    [Range(0, 1)]
    [SerializeField] float debugT = 0;
    public bool isAnimating = false;
    bool shownAnimation = false;

    private void OnValidate()
    {
        if (rectTransform == null)
            rectTransform = this.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        if(!shownAnimation||!isAnimating)
            StartCoroutine(AnimateButton());
        //Debug.Log("Animation Called ");
    }

    void Start()
    {
        shownAnimation = false;
        rectTransform = this.GetComponent<RectTransform>();


        //GetInitialPosition();
        //animThread = new Thread(() =>
        //{
        //    Debug.Log($"{gameObject.name} Thread Started");
        //    rectTransform.position = Vector3.zero;
        //    while (rectTransform.position != targetPosition)
        //    {
        //    Debug.Log($"speed {speed}");
        //        speed += 1;
        //        rectTransform.localPosition = Vector3.Lerp(Vector3.zero, targetPosition, speed);

        //    }
        //});
        //animThread.Start();
        StartCoroutine(AnimateButton());
    }


    private void OnDisable()
    {
        isAnimating = false;
        shownAnimation = false;
    }

    private IEnumerator AnimateButton()
    {
        if (isAnimating || shownAnimation)
        {
            //Debug.Log("did not Went through");
            yield break;
        }
        //Debug.Log("Went through");
        shownAnimation = true;
        isAnimating = true;
        //Debug.Log($"{gameObject.name} Thread Started");
        var distance = Vector3.Distance(rectTransform.localPosition, targetPosition);
        var time = 0f;
        float t = 0;

        //while (distance > 0.1) 
        while (time < animTime)
        {
            //Debug.Log($"{gameObject.name} distance {distance} ");
            //Debug.Log($"speed {speed}");
            //speed += 1*Time.deltaTime;
            //var t = 1 - (Time.deltaTime / 2);
            t += 1f * Time.deltaTime;
            rectTransform.localPosition = Vector3.Lerp(initPosition, targetPosition, t);
            //Debug.Log($"{gameObject.name} t {t} ");
            yield return null;
            time += Time.deltaTime;
        }
        isAnimating = false;

    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(initPosition, debugSphereSize);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, debugSphereSize);
    }

    public bool useDebugger = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(AnimateButton());
        }
        if (useDebugger)
        {
            rectTransform.localPosition = Vector3.Lerp(initPosition, targetPosition, debugT);
        }
    }

    [ContextMenu("Set Current as Target Position")]
    public void GetInitialPosition()
    {
        targetPosition = rectTransform.localPosition;
    }


    #region Tools


    #endregion

}