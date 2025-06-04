using UnityEngine;

public class Bat : MonoBehaviour, Weapons.IWeapon
{
    [SerializeField] private Animator animator;
    private float meleeSpeed = 2f;
    private float damage = 10f;

    private int attackQueue;

    private bool isAttacking = false;
    Quaternion rotationGoal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        if (isAttacking) return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        rotationGoal = Quaternion.LookRotation(transform.forward, (new Vector3(mousePosition.x, mousePosition.y, 0) - transform.position));
        rotationGoal = Quaternion.Euler(0, 0, rotationGoal.eulerAngles.z + 90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationGoal, 1000 * Time.deltaTime);
    }

    private void UnlockLockRotation()
    {
        isAttacking = !isAttacking;
        Debug.Log(isAttacking);
    }

    public void Attack()
    {

        animator.SetTrigger("Attack_Chain1");
    }
}
