using NUnit.Framework.Constraints;
using UnityEngine;
using Weapons;

public class Bat : Weapon, IWeapon
{
    [SerializeField] private Animator animator;
    private float meleeSpeed = 2f;
    private float damage = 10f;
    private float knockBackForce = 10f;
    private float attackTimeout = 0.5f;
    private float swingDelay = 0.1f;

    private int attackQueue = 0;
    private int currentComboStage = 0;
    private float timer = 0;

    private bool inAttack = false;

    private void Update()
    {
        Attack();
        AttackTimeout();
    }

    public void QueueAttack()
    {
        if (attackQueue < 3)
        {
            attackQueue++;
        }
    }

    private void AttackTimeout()
    {
        timer -= Time.deltaTime;
    }

    private void Attack()
    {
        if (timer > 0) return;

        if (inAttack) return;

        if (attackQueue <= 0) 
        {
            currentComboStage = 0;
            attackQueue = 0;
            return;
        }


        switch (currentComboStage) 
        { 
            case 0:
                animator.SetTrigger("Attack_Chain1");
                currentComboStage++;
                break;
            case 1:
                animator.SetTrigger("Attack_Chain2");
                currentComboStage++;
                break;
            case 2:
                animator.SetTrigger("Attack_Chain3");
                currentComboStage = 0;
                attackQueue = 0;
                timer = attackTimeout;
                break;
        }

        attackQueue--;
    }

    private void ToggleAttack()
    {
        inAttack = !inAttack;
        timer = swingDelay;
    }
}
