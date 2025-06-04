using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private float deceleration = 10f;
    //[SerializeField] private float deceleration = 5f;

    [Header("Components")]
    [SerializeField] GameObject interactComponent;
    [SerializeField] GameObject weapon;

    public bool isPushed = false;

    private Vector2 movementVector = Vector2.zero;
    private Rigidbody2D rigidBody;


    private List<GameObject> targets = new List<GameObject>();

    private int attackQueue = 0;
    private int comboCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotateTowardsMouse();
    }

    private void MovePlayer()
    {
        if (movementVector != Vector2.zero)
        {
            rigidBody.linearVelocity = movementVector * speed;
        } 
        else
        {
            SlowDownPlayer();
        }
    }

    private void RotateTowardsMouse()
    {
        Quaternion rotationGoal;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        rotationGoal = Quaternion.LookRotation(interactComponent.transform.forward, (new Vector3(mousePosition.x, mousePosition.y, 0) - interactComponent.transform.position));
        rotationGoal = Quaternion.Euler(0, 0, rotationGoal.eulerAngles.z + 90);
        interactComponent.transform.rotation = Quaternion.RotateTowards(interactComponent.transform.rotation, rotationGoal, rotationSpeed * Time.deltaTime);
    }

    private void SlowDownPlayer()
    {
        Vector3 speedVector = rigidBody.linearVelocity;
        Vector3 invertedSpeedVector = speedVector * -1 * deceleration * Time.deltaTime;

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

        rigidBody.linearVelocity = speedVector;
    }

    private void OnAttack()
    {
        attackQueue++;
        weapon.GetComponent<Bat>().Attack();
    }

    private void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
    }

    private void OnInteract(InputValue value)
    {
        GetComponentInChildren<BoxCollider2D>().enabled = true;
        Invoke("DisableHitBox", 0.2f);
    }

    private void DisableHitBox()
    {
        GetComponentInChildren<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            //Trigger cutscene
        }

        if (collision.gameObject.tag == "NPC-Dynamic")
        {
            Debug.Log("Test");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.gameObject.transform.position - gameObject.transform.position).normalized * 80, ForceMode2D.Impulse);
        }
    }
}
