using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script put on environment to uncheck selected turret when player clicks on it.
/// </summary>
public class CheckClicks : MonoBehaviour
{

    Shop shop;
    StatsUI statsUI;

    private void Awake()
    {
        shop = FindObjectOfType<Shop>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject() || !shop.shopUI.activeSelf)
        {
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
