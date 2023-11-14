using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    private PlaceIndicator placeIndicator;
    public GameObject[] objectToPlace;
    public GameObject placeButton;
    public GameObject toolsPanel;

    private GameObject newPlacedObject;

    void Start()
    {
        placeIndicator = FindObjectOfType<PlaceIndicator>(); 
    }
    private void Update() 
    {
        if(placeIndicator.isIndicatorThere == false)
        {
            placeButton.SetActive(false);
            toolsPanel.SetActive(false);
        }
        else
        {
            placeButton.SetActive(true);
            toolsPanel.SetActive(true);
        }
    }

    public void PlaceObject()
    {
        newPlacedObject = Instantiate(objectToPlace[PlayerPrefs.GetInt("selectedTool",0)], placeIndicator.transform.position, placeIndicator.transform.rotation);
    }
}
