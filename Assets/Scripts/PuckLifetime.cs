using EyeFactiveMarkerManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckLifetime : MonoBehaviour
{
    [SerializeField] InteractablePuck interactablePuck;
    [SerializeField] float lifetime = 0;
    [SerializeField] float maxLifetime = 5;
    // Start is called before the first frame update
    void Start()
    {
        interactablePuck = GetComponent<InteractablePuck>();
        //interactablePuck = gameObject.AddComponent<InteractablePuck>();
    }
    private void OnEnable()
    {
        lifetime = maxLifetime;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!MarkerManager.Instance.ExistPuck(interactablePuck.GetPuckID))
        {
            lifetime -= 1 * Time.deltaTime;
            if (lifetime <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
