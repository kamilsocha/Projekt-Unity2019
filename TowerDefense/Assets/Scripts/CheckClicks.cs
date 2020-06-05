using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script put on environment to uncheck selected turret when player clicks on it.
/// </summary>
public class CheckClicks : MonoBehaviour
{
    StatsUI statsUI;

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("Clicked on the UI");
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
