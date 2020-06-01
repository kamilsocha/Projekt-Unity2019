using System.Collections;
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

        buyAnimator = buyAnimationGO.GetComponentInChildren<Animator>();
        buyAnimationText = buyAnimationGO.GetComponentInChildren<TMP_Text>();
    }
    #endregion

    TurretBlueprint turretToBuild;
    Node selectedNode;
    public NodeUI nodeUI;
    public GameObject buyAnimationGO;
    TMP_Text buyAnimationText;
    Animator buyAnimator;
    public RangeDrawer rangeDrawer;

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
        buyAnimationGO.SetActive(true);
        buyAnimationGO.transform.position = pos;
        buyAnimationText.text = text;
        StartCoroutine(PlayBuyAnimation());
    }

    IEnumerator PlayBuyAnimation()
    {
        buyAnimator.SetTrigger("Buy");
        var state = buyAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);
        buyAnimationGO.SetActive(false);
    }

}
