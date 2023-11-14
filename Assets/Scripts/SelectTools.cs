using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTools : MonoBehaviour
{
    public GameObject[] tools;
    public int selectedTool = 0;
    public GameObject selectToolPanel;

    private void Awake() 
    {
        foreach(GameObject body in tools)
            body.SetActive(false);
        tools[selectedTool].SetActive(true);
    }

    public void ChangeNext()
    {
        
        tools[selectedTool].SetActive(false);
        selectedTool++;
        if (selectedTool == tools.Length)
            selectedTool = 0;

        tools[selectedTool].SetActive(true);
        PlayerPrefs.SetInt("selectedTool",selectedTool);
    }

    public void ChangePrevious()
    {
        
        tools[selectedTool].SetActive(false);
        selectedTool--;
        if (selectedTool == -1)
            selectedTool = tools.Length-1;

        tools[selectedTool].SetActive(true);
        PlayerPrefs.SetInt("selectedTool",selectedTool);
    }

    public void SelectBody()
    {
        selectToolPanel.SetActive(false);
    }
}
