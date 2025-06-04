using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistanceFromPlayer;

    private Vector2 lastPlayerLocation;
    private Vector2 closestWaypoint;

    void Start()
    {
        
    }

   
    void Update()
    {
        if ((lastPlayerLocation - (Vector2)transform.position).magnitude + 0.05f < minDistanceFromPlayer) 
        {
            lastPlayerLocation = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PLayer")) 
        {
            lastPlayerLocation = collision.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Waypoint"))
        {

        }
    }
}
