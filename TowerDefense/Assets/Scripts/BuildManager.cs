﻿using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Singleton

    public static BuildManager Instance { get; private set; }
    
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Error - more than one instance of BuildManager");
            return;
        }
        Instance = this;
    }
    #endregion

    TurretBlueprint turretToBuild;
    Node selectedNode;

    public GameObject buildEffect;

    public bool CanBuild
    {
        get
        {
            return turretToBuild != null;
        }
    }
    public bool HasMoney
    {
        get
        {
            return PlayerStats.Money >= turretToBuild.cost;
        }
    }


    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
    }

    public void DeselectNode()
    {
        selectedNode = null;
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
