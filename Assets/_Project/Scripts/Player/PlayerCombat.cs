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

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TryAttack(AttackType attackType)
    {
            
        if (isAttacking)
        {
            return false;
        }

        Debug.Log("Try Attack");
            
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
        Debug.Log("Desenfundo y ataco");
      
        playerAnimator.PlayUnsheathe(true);
    }
    public void FinishAttack()
    {
        isAttacking = false;
    }
    public void Unsheathe()
    {
        isWeaponDrawn = true;
        Debug.Log("Desenfundo");
       
        playerAnimator.PlayUnsheathe(false);
    }
    public void Sheathe()
    {
        isWeaponDrawn = false;
        Debug.Log("Enfundo");
        playerAnimator.PlaySheathe();
    }
}
