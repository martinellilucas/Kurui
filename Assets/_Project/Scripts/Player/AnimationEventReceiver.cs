using UnityEngine;


    public class AnimationEventReceiver : MonoBehaviour
    {
        private PlayerCombat playerCombat;
        private PlayerAnimator playerAnimator;
        private Weapon weapon;
        private WeaponHitbox hitbox;
       

        private void Awake()
        {
            playerCombat = GetComponentInParent<PlayerCombat>();
            playerAnimator = GetComponentInParent<PlayerAnimator>();
            weapon = GetComponentInChildren<Weapon>();
            hitbox = GetComponentInChildren<WeaponHitbox>();
          
        }

    public void FinishAttack()
    {
        playerCombat.FinishAttack();
        playerAnimator.SetAttackAfterUnsheathe(false);

    }
    public void EquipWeapon()
    {
        weapon.Equip();
    }

    public void UnequipWeapon()
    {
        weapon.Unequip();
    }

    public void EnableWeaponHitbox()
    {
        hitbox.EnableHitbox();
    }
    public void DisableWeaponHitbox()
    {
        hitbox.DisableHitbox();
    }
}

