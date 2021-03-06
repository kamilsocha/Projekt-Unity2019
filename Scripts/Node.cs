﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages state of node, allows to upgrade or sell turret placed on the node.
/// Displays if player can perform transaction with adequate hover color.
/// Plays effects linked with specific transactions.
/// </summary>
public class Node : MonoBehaviour
{
    Color startColor;
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    MeshRenderer rend;
    BuildManager buildManager;
    public Vector3 positionOffset;
    public bool isUpgraded = false;
    public GameObject Turret { get; set; }
    public TurretBlueprint TurretBlueprint { get; set; }

    public PlayerStats playerStats;
    

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        startColor = rend.material.GetColor("_BaseColor");

        buildManager = BuildManager.Instance;
        playerStats = GameObject.Find("GameMaster").GetComponent<PlayerStats>();
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
                
        if(Turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        if (!buildManager.CanBuild) return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if(playerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        playerStats.ReduceMoney(blueprint.cost);

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        Turret = _turret;

        TurretBlueprint = blueprint;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        buildManager.BuyTextAnimation("$" + blueprint.cost, GetBuildPosition());

    }

    public void UpgradeTurret()
    {
        if(playerStats.Money < TurretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade!");
            return;
        }
        playerStats.ReduceMoney(TurretBlueprint.upgradeCost);

        Destroy(Turret);

        GameObject _turret = Instantiate(TurretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        Turret = _turret;

        GameObject upgradeEffectGO = Instantiate(buildManager.upgradeEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(upgradeEffectGO, 5f);
        buildManager.BuyTextAnimation("$" + TurretBlueprint.upgradeCost, GetBuildPosition());

        isUpgraded = true;
    }

    public void SellTurret()
    {
        if (!isUpgraded)
        {
            playerStats.RestoreMoney(TurretBlueprint.GetSellAmount());
        }
        else
        {
            playerStats.ReduceMoney(TurretBlueprint.GetUpgradedSellAmount());
        }

        GameObject sellEffectGO = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(sellEffectGO, 5f);

        Destroy(Turret);
        TurretBlueprint = null;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (!buildManager.CanBuild) return;

        if(buildManager.CanBuy)
        {
            rend.material.SetColor("_BaseColor", hoverColor);
        }
        else
        {
            rend.material.SetColor("_BaseColor", notEnoughMoneyColor);
        }

        buildManager.DrawTurretRange(GetBuildPosition());
    }

    private void OnMouseExit()
    {
        rend.material.SetColor("_BaseColor", startColor);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        buildManager = BuildManager.Instance;
        playerStats = FindObjectOfType<PlayerStats>();
    }
}
