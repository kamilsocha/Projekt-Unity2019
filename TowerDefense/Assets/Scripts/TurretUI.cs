using UnityEngine;

public class TurretUI : MonoBehaviour
{
    public GameObject ui;
    Animator animator;

    private void Awake()
    {
        ui.SetActive(false);
        animator = GetComponent<Animator>();
    }

    public void Show()
    {
        ui.SetActive(true);
        animator.SetTrigger("In");
    }

    public void Hide()
    {
        animator.SetTrigger("Out");
    }

    public void Deactivate() { ui.SetActive(false); }
}
