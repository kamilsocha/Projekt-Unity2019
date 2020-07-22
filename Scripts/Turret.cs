using UnityEngine;

/// <summary>
/// Base class for all turrets.
/// Contains general information about turret as well as basic properties.
/// </summary>
public abstract class Turret : MonoBehaviour
{
    [Header("General")]
    public float range = 15f;
    public float fireRate = 1f;
    public float damage;
    public string description;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
