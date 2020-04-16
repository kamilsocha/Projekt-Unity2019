using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 5f;
    [HideInInspector]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
