using EyeFactiveMarkerManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuckImageChnager : MonoBehaviour
{
    [SerializeField] Sprite[] Pucks;
    [SerializeField] Image CurrentPuck;
    [SerializeField] InteractablePuck interactablePuck;

    private void OnEnable()
    {
        interactablePuck = GetComponent<InteractablePuck>();
        ChangePuck();  
    }


    public void ChangePuck()
    {
        int p_puckID = interactablePuck.GetPuckID;
        int puck_index = MarkerManager.GetMarkerIDToIndex(p_puckID);
        CurrentPuck.sprite = Pucks[puck_index];
    }
    // Start is called before the first frame update
    void Start()
    {
        interactablePuck = GetComponent<InteractablePuck>();
        ChangePuck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
