using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject indicator;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        indicator = transform.GetChild(0).gameObject;

        indicator.SetActive(false);
    }

    void Update()
    {
        var ray = new Vector2(Screen.width / 2 , Screen.height / 2);
        if(rayManager.Raycast(ray, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;

            transform.position = hitPose.position;
            transform.rotation = hitPose.rotation;

            if(!indicator.activeInHierarchy)
            {
                indicator.SetActive(true);
            }
        }
    }
}