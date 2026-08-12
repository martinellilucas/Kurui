using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private bool isAttacking;
    private bool isWeaponDrawn;
    private AttackType pendingAttack;
    public enum AttackType { 
        
        Attack01
        };

    private PlayerAnimator playerAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }


    public bool TryAttack(AttackType attackType)
    {
            
        if (isAttacking)
        {
            return false;
        }
            
        isAttacking = true;
        if (isWeaponDrawn)
        {
            StartAttack(attackType);
        }
        else
        {
            pendingAttack = attackType;
            UnsheatheAndAttack();
        }
        return isAttacking;

        
    }
    private void StartAttack(AttackType attackType)
    {
        switch (attackType)
        {
           
            case AttackType.Attack01:
               
                playerAnimator.PlayAttack();
                break;

        }
    }

    public void UnsheatheAndAttack()
    {
        isWeaponDrawn = true;
        playerAnimator.PlayUnsheathe(true);
    }
    public void FinishAttack()
    {
        isAttacking = false;
    }
    public void Unsheathe()
    {
        isWeaponDrawn = true;       
        playerAnimator.PlayUnsheathe(false);
    }
    public void Sheathe()
    {
        isWeaponDrawn = false;
        playerAnimator.PlaySheathe();
    }
}
