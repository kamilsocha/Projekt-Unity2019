using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CheckClicks : MonoBehaviour
{
    StatsUI statsUI;

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Clicked on the UI");
            return;
        }

        BuildManager.Instance.DeselectTurretToBuild();
        if(statsUI == null)
        {
            statsUI = GameObject.Find("StatsUI").GetComponent<StatsUI>();
        }
        statsUI.Hide();
        
    }

}
