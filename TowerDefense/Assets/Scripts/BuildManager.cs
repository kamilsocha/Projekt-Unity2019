using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    //public GameObject buyText;
    public RangeDrawer rangeDrawer;
    //public GameObject rangeDrawer;

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

    public void DrawTurretRange(Vector3 pos)
    {
        var turret = turretToBuild.prefab.GetComponent<Turret>() as Turret;
        rangeDrawer.Show(pos, turret.range);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        rangeDrawer.Hide();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "LevelShared")
        {
            nodeUI = GameObject.Find("NodeUI").GetComponent<NodeUI>();
            nodeUI.Hide();
        }
    }

    public void BuyTextAnimation(string text, Vector3 pos)
    {
        /*buyText.SetActive(true);
        buyText.transform.position = pos;
        buyText.GetComponent<TMP_Text>().text = text;
        GetComponent<Animator>().SetTrigger("Buy");*/
    }

}
