using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    private bool isAttacking = false;
    Quaternion rotationGoal;

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

    public void InitiateAttack()
    {
        BroadcastMessage("Attack");
    }
}
