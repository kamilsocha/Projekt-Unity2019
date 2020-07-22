using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script put on environment to uncheck selected turret when player clicks on it.
/// </summary>
public class CheckClicks : MonoBehaviour
{

    Shop shop;
    BuildManager buildManager;
    StatsUI statsUI;

    private void Start()
    {
        shop = FindObjectOfType<Shop>();
        buildManager = BuildManager.Instance;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject() || !shop.shopUI.activeSelf)
        {
            return;
        }
        buildManager.DeselectTurretToBuild();
        buildManager.DeselectNode();

        if(statsUI == null)
        {
            statsUI = GameObject.Find("StatsUI").GetComponent<StatsUI>();
        }
        statsUI.Hide();
    }

}
