using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Responsible for building turrets, informing if transactions are possible.
/// Manages range drawer to display adequate range when placing or selecting turret.
/// Provides effects for node to play. Playes floating money animation when turret is bought.
/// </summary>
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
    ObjectPooler objectPooler;
    public GameObject rangeDrawerPrefab;
    public AudioManager audioManager;
    RangeDrawer rangeDrawer;

    public GameObject buildEffect;
    public GameObject upgradeEffect;
    public GameObject sellEffect;
    PlayerStats playerStats;

    string transactionSound = "Coin";

    public bool CanBuild
    {
        get
        {
            return turretToBuild != null;
        }
    }
    public bool CanBuy
    {
        get
        {
            return playerStats.Money >= turretToBuild.cost;
        }
    }
    public bool CanUpgrade
    {
        get
        {
            return playerStats.Money >= selectedNode.TurretBlueprint.upgradeCost;
        }
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        audioManager = AudioManager.Instance;
        rangeDrawer = Instantiate(rangeDrawerPrefab, transform.position, Quaternion.identity).GetComponent<RangeDrawer>();
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

        DrawTurretRange();

        nodeUI.SetTarget(node);
    }

    void DrawTurretRange()
    {
        var turret = selectedNode.Turret.GetComponent<Turret>() as Turret;
        rangeDrawer.Show(selectedNode.GetBuildPosition(), turret.range);
    }

    public void DrawTurretRange(Vector3 pos)
    {
        var turret = turretToBuild.prefab.GetComponent<Turret>() as Turret;
        rangeDrawer.Show(pos, turret.range);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
        rangeDrawer.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        rangeDrawer.Hide();
        return turretToBuild;
    }

    public void DeselectTurretToBuild()
    {
        if(turretToBuild != null)
        {
            rangeDrawer.Hide();
            turretToBuild = null;
        } 
    }

    private void OnEnable()
    {
        turretToBuild = null;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "LevelShared")
        {
            nodeUI = GameObject.Find("NodeUI").GetComponent<NodeUI>();
            nodeUI.Hide();
            playerStats = FindObjectOfType<PlayerStats>();
        }
    }

    public void BuyTextAnimation(string text, Vector3 pos)
    {
        GameObject floatingMoney = objectPooler.SpawnFromPool("BuyAnimations", pos, Quaternion.identity);
        floatingMoney.GetComponentInChildren<TMP_Text>().text = text;
        audioManager.Play(transactionSound);
    }

}
