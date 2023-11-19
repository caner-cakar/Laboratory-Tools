using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class PlaceManager : MonoBehaviour
{
    private PlaceIndicator placeIndicator;
    public GameObject[] objectToPlace;
    public GameObject placeButton;
    public GameObject destroyButton;
    public GameObject refreshButton;
    public GameObject toolsPanel;

    private GameObject selectedGameObject;

    private GameObject newPlacedObject;

    [SerializeField] ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Camera arCam;

    void Start()
    {
        placeIndicator = FindObjectOfType<PlaceIndicator>();
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (placeIndicator.isIndicatorThere == false)
        {
            destroyButton.SetActive(false);
            placeButton.SetActive(false);
            toolsPanel.SetActive(false);
            refreshButton.SetActive(false);
        }
        else
        {
            placeButton.SetActive(true);
            toolsPanel.SetActive(true);
            refreshButton.SetActive(true);
        }

        if(Input.touchCount == 0)
        {
            return;
        }
        
        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if(aRRaycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.LogError("Touched");
                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.gameObject.tag == "PlacedObject")
                    {
                        ToggleTextTMPActivation(hit.collider.gameObject);
                        selectedGameObject = hit.collider.gameObject;
                        destroyButton.SetActive(true);
                        Debug.LogError("Placedobject");
                    }
                    else
                    {
                        destroyButton.SetActive(false);
                        Debug.LogError("Error: not a placedobject");
                    }
                }
            }
        }
    }

    public void PlaceObject()
    {
        newPlacedObject = Instantiate(objectToPlace[PlayerPrefs.GetInt("selectedTool", 0)], placeIndicator.transform.position, placeIndicator.transform.rotation);
        ToggleTextTMPActivation(newPlacedObject);
    }

    private void ToggleTextTMPActivation(GameObject gameObject)
    {
        if (gameObject == null)
        {
            Debug.LogError("GameObject is null");
            return;
        }

        Transform textMesh = gameObject.transform.Find("Text (TMP)");

        if (textMesh!=null)
        {
            if (textMesh.gameObject.activeSelf)
            {
                textMesh.gameObject.SetActive(false);
                Debug.LogError("Text not active");
            }
            else
            {
                textMesh.gameObject.SetActive(true);
                Debug.LogError("Text active");
            }
        }
        else
        {
            Debug.LogError("TextMeshPro is null");
        }
    }

    public void DestroyObject()
    {
        Destroy(selectedGameObject);
        destroyButton.SetActive(false);
    }

    public void RefreshButton()
    {
        SceneManager.LoadScene("ARScene");
    }
}
