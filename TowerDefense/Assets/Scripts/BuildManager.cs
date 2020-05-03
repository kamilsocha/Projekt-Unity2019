using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildManager : MonoBehaviour
{
    #region Singleton

    public static BuildManager Instance { get; private set; }
    
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("BuildManager gets destroyed.");
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }
    #endregion

    TurretBlueprint turretToBuild;
    Node selectedNode;
    public NodeUI nodeUI;

    public GameObject buildEffect;
    public GameObject upgradeEffect;
    public GameObject sellEffect;

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

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "LevelShared")
        {
            nodeUI = GameObject.Find("NodeUI").GetComponent<NodeUI>();
        }
    }

}
