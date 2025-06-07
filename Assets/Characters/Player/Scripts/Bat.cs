using UnityEngine;
using Weapons;

public class Bat : Weapon, IWeapon
{
    [SerializeField] private Animator animator;
    private float meleeSpeed = 2f;
    private float damage = 10f;

    private int attackQueue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        animator.SetTrigger("Attack_Chain1");
    }
}
