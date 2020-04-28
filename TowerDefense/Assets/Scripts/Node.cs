using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    Color startColor;
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    Renderer rend;
    public BuildManager buildManager;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject Turret { get; set; }
    [HideInInspector]
    public TurretBlueprint TurretBlueprint { get; set; }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.Instance;
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
        //TODO money

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        Turret = _turret;

        TurretBlueprint = blueprint;

        //TODO effect
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (!buildManager.CanBuild) return;

        if(buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
