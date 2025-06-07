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

        transform.position = Vector2.MoveTowards(transform.position, lastPlayerLocation, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
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
