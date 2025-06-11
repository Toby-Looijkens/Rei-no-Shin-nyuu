using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float minDistanceFromPlayer;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private GameObject player;

    [SerializeField] Rigidbody2D rigidBody;

    [Header("Stats")]
    [SerializeField] private float health = 100;
    [SerializeField] private float damage = 5;
    [SerializeField] private float attackCooldown = 1.8f;
    [SerializeField] private float speed;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float slowDownCutOff = 0.08f;
    [SerializeField] private float minDistanceFromTarget = 0.05f;

    private Vector2 lastPlayerLocation;
    private Vector2 closestWaypoint;

    private bool canSeePlayer = false;

    void Start()
    {
        
    }

   
    void Update()
    {
        MoveToLocation(player.transform.position);
    }

    private void MoveToLocation(Vector2 destination)
    {
        if (rigidBody.linearVelocity.magnitude > 0)
        {
            SlowDown();
            return;
        }

        Vector2 v2 = transform.position;
        if ( (v2 - destination).magnitude > minDistanceFromTarget )
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
    }

    private void SlowDown()
    {
        Vector3 speedVector = rigidBody.linearVelocity;
        Vector3 invertedSpeedVector = speedVector * -deceleration;

        if (Mathf.Abs(speedVector.x) >= 0 && Mathf.Abs(speedVector.x) <= Mathf.Abs(invertedSpeedVector.x))
        {
            speedVector.x = 0;
        }
        else
        {
            speedVector.x += invertedSpeedVector.x;
        }

        if (Mathf.Abs(speedVector.y) >= 0 && Mathf.Abs(speedVector.y) <= Mathf.Abs(invertedSpeedVector.y))
        {
            speedVector.y = 0;
        }
        else
        {
            speedVector.y += invertedSpeedVector.y;
        }

        if (speedVector.magnitude <= 0.08f)
        {
            rigidBody.linearVelocity = Vector2.zero;
        } 
        else
        {
            rigidBody.linearVelocity = speedVector;
        }
    }

    public void TakeDamage(float damage, float knockback)
    {
        rigidBody.AddForce((transform.position - player.transform.position) * knockback);
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
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
